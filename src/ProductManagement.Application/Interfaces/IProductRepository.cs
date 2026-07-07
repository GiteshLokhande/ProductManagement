using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetProductWithItemsAsync(int productId);
    }
}