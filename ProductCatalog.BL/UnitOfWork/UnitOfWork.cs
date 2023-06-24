using ProductCatalog.BL.Repository;
using ProductCatalog.DAL.Context;
using ProductCatalog.DAL.Models;

namespace ProductCatalog.BL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductCatalogContext _context;
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }

        public UnitOfWork(ProductCatalogContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(_context);
            Categories = new GenericRepository<Category>(_context);
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync().ConfigureAwait(false) > 0;

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
