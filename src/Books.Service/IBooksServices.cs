using Books.Model;
using Books.Model.DTOs;
using Books.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.Service
{
    public interface IBooksService : IService
    {
        Task<List<BookEntity>> GetBooksAsync();
        Task<int> GetTotalBooksAsync();
        Task<bool> RemoveAsync(BookEntity book);
        Task<bool> UpdateToReadStatusAsync(BookEntity book);
        Task<BookEntity> AddBookAsync(BookEntity bookToAdd);
    }
}
