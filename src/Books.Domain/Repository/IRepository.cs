using System.Linq;
using System.Threading.Tasks;

namespace Books.Domain.Repository
{
    public interface IRepository<T>
    {
        void Update(T entity);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task<T> FindAsync(object id);
        Task<T> FindAsync(object id1, object id2);
        IQueryable<T> Query { get; }
        Task SaveAsync();
    }
}
