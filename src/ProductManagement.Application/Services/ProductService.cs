using AutoMapper;
using ProductManagement.Application.Constants;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            product.CreatedBy = _currentUserService.UserName ?? ApplicationConstants.SystemUser;
            product.CreatedOn = DateTime.UtcNow;

            await _unitOfWork.Products.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} was not found.");

            _unitOfWork.Products.Delete(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} was not found.");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException($"Product with Id {id} was not found.");

            product.ProductName = dto.ProductName;
            product.ModifiedBy = _currentUserService.UserName ?? ApplicationConstants.SystemUser;
            product.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}