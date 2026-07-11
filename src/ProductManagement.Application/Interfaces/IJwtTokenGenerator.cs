using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AuthenticatedUserDto user, IList<string> roles);
    }
}