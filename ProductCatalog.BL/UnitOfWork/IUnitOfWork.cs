using ProductCatalog.BL.Repository;
using ProductCatalog.DAL.Models;

namespace ProductCatalog.BL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        Task<bool> SaveChangesAsync();
    }
}
