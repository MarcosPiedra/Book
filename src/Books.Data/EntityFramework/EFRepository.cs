using Books.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Data.EntityFramework
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly BooksContext dbContext;

        public EFRepository(BooksContext dbContext)
        {
            this.dbSet = dbContext.Set<T>();
            this.dbContext = dbContext;
        }

        public async Task AddAsync(T item) => await dbSet.AddAsync(item);
        public void Remove(T item) => dbSet.Remove(item);
        public IQueryable<T> Query => dbSet.AsNoTracking();
        public void Update(T item) => dbSet.Update(item);
        public async Task<T> FindAsync(object id) => await dbSet.FindAsync(id);
        public async Task<T> FindAsync(object id1, object id2) => await dbSet.FindAsync(id1, id2);
        public async Task SaveAsync() => await dbContext.SaveChangesAsync();
    }
}
