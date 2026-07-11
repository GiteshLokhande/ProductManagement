using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Product> Products, int TotalRecords)> GetPagedAsync(PaginationRequestDto request)
        {
            IQueryable<Product> query = _context.Products.AsNoTracking();

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(p =>
                    p.ProductName.Contains(request.Search));
            }

            // Total count before pagination
            var totalRecords = await query.CountAsync();

            // Sorting
            query = (request.SortBy?.ToLower(), request.SortOrder?.ToLower()) switch
            {
                ("productname", "desc") => query.OrderByDescending(x => x.ProductName),
                ("productname", _) => query.OrderBy(x => x.ProductName),

                ("createdon", "desc") => query.OrderByDescending(x => x.CreatedOn),
                ("createdon", _) => query.OrderBy(x => x.CreatedOn),

                ("id", "desc") => query.OrderByDescending(x => x.Id),

                _ => query.OrderBy(x => x.Id)
            };

            // Pagination
            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (products, totalRecords);
        }
    }
}