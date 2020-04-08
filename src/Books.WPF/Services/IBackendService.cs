using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.WPF.Services
{
    public interface IBackendService
    {
        Task<User> GetTokenAsync(string user, string password);
        Task<int> GetCountBooksAsync(User user);
        Task<List<Book>> GetBooksAsync(User user, int from, int to);
        Task<Book> SaveBookAsync(User user, Book book);
        Task<Book> NewBookAsync(User user, Book book);
    }
}
