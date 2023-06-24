using Microsoft.EntityFrameworkCore;
using ProductCatalog.DAL.Context;
using ProductCatalog.DAL.Models;
using System.Linq.Expressions;

namespace ProductCatalog.BL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseObject
    {
        private readonly ProductCatalogContext _context;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(ProductCatalogContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _entities.ToListAsync();

        public async Task<TEntity> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public void Add(TEntity entity) => _entities.Add(entity);

        public TEntity Update(TEntity entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(TEntity entity) => _entities.Remove(entity);

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.Where(predicate).ToListAsync();
    }
}
