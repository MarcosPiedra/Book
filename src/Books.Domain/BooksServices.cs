using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Books.CrossCutting;
using Books.Domain;
using Books.Domain.Entities;
using Books.Domain.Repository;

namespace Books.Domain
{
    public class BooksService : IBooksService
    {
        private readonly IRepository<Book> _repo;
        private readonly ILogger _logger;

        public BooksService(IRepository<Book> repo
                          , ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<Book>> GetBooksAsync() => await Task.FromResult(_repo.Query.ToList());

        public async Task<int> GetTotalBooksAsync()
        {
            var bookList = await GetBooksAsync();

            return await Task.FromResult(bookList.Count);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _repo.AddAsync(book);
            await _repo.SaveAsync();

            return book;
        }

        public async Task<bool> RemoveAsync(Book book)
        {
            _repo.Remove(book);
            await _repo.SaveAsync();

            return true;
        }

        public async Task<bool> UpdateToReadStatusAsync(Book book)
        {
            book.IsRead = true;

            _repo.Update(book);
            await _repo.SaveAsync();

            return true;
        }
    }
}
