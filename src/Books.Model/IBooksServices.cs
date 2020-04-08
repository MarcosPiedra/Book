using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.Service
{
    public interface IBooksService : IService
    {
        Task<List<Book>> GetBooksAsync();
        Task<int> GetTotalBooksAsync();
        Task<bool> RemoveAsync(Book book);
        Task<bool> UpdateToReadStatusAsync(Book book);
        Task<Book> AddBookAsync(Book bookToAdd);
    }
}
