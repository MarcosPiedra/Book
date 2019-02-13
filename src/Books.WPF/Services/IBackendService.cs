using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Model;
using Books.Model.Entities;

namespace Books.WPF.Services
{
    public interface IBackendService
    {
        Task<User> GetTokenAsync(string user, string password);
        Task<int> GetCountBooksAsync(User user);
        Task<List<BookEntity>> GetBooksAsync(User user, int from, int to);
        Task<BookEntity> SaveBookAsync(User user, BookEntity book);
        Task<BookEntity> NewBookAsync(User user, BookEntity book);
    }
}
