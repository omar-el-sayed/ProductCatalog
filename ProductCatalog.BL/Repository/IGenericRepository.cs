using ProductCatalog.DAL.Models;
using System.Linq.Expressions;

namespace ProductCatalog.BL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseObject
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        void Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
