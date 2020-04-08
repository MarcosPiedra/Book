using Books.Model.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Repository
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        IQueryable<T> Query();
        Task<EntityEntry<T>> AddAsync(T entity);
        T Remove(T entity);
        void Edit(T entity);
        Task<int> SaveAsync();
    }
}
