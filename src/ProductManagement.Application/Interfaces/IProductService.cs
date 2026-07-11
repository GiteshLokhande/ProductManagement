using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDto>> GetAllAsync(PaginationRequestDto request);

        Task<ProductDto?> GetByIdAsync(int id);

        Task<ProductDto> CreateAsync(CreateProductDto dto);

        Task UpdateAsync(int id, UpdateProductDto dto);

        Task DeleteAsync(int id);
    }
}