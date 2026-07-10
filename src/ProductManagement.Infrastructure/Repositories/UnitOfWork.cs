using ProductManagement.Application.Interfaces;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; }

        public IRefreshTokenRepository RefreshTokens { get; }

        public UnitOfWork(ApplicationDbContext context,IProductRepository productRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _context = context;

            Products = productRepository;

            RefreshTokens = refreshTokenRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
