using Books.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.UnitOfWork
{
    public interface IUOW : IDisposable
    {
        IBooksRepository BooksRepository { get; }

        Task<int> CommitAsync();
    }
}
