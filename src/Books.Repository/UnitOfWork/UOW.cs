using Books.Data.EntityFramework.Context;
using Books.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Books.UnitOfWork
{
    public class UOW : IUOW
    {
        DbContext _dbContext;

        public UOW(BooksContext context)
        {
            _dbContext = context;
            BooksRepository = new BooksRepository(context);
        }

        public IBooksRepository BooksRepository { get; internal set; }

        public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}