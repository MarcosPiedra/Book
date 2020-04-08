using Books.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
          where T : BaseEntity
    {
        protected DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> Query() => _dbSet.AsQueryable();
        public virtual Task<EntityEntry<T>> AddAsync(T entity) => _dbSet.AddAsync(entity);
        public virtual T Remove(T entity) => _dbSet.Remove(entity).Entity;
        public virtual void Edit(T entity) => _dbContext.Entry(entity).State = EntityState.Modified;
        public virtual Task<int> SaveAsync() => _dbContext.SaveChangesAsync();
    }
}
