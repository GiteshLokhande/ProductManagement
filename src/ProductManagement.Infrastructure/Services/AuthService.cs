using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProductManagement.Application.Constants;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Identity;
using ProductManagement.Infrastructure.Repositories;
using ProductManagement.Infrastructure.Security;

namespace ProductManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IOptions<JwtSettings> jwtSettings,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordValid)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var authenticatedUser = new AuthenticatedUserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!
            };

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(authenticatedUser, roles);

            var refreshToken = RefreshTokenGenerator.Generate();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };
        }
        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ValidationException(errors);
            }

            await _userManager.AddToRoleAsync(user, Roles.User);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(dto.RefreshToken);

            if (refreshToken == null)
                throw new UnauthorizedAccessException("Invalid refresh token.");

            if (refreshToken.IsRevoked)
                throw new UnauthorizedAccessException("Refresh token has been revoked.");

            if (refreshToken.ExpiresOn <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token has expired.");

            var user = await _userManager.FindByIdAsync(refreshToken.UserId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            var authenticatedUser = new AuthenticatedUserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!
            };

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtTokenGenerator.GenerateToken(authenticatedUser, roles);

            var newRefreshToken = RefreshTokenGenerator.Generate();

            refreshToken.IsRevoked = true;

            var refreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };
        }
    }
}
