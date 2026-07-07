using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository
    : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<Product?> GetProductWithItemsAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}