using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Books.UnitOfWork;
using Books.CrossCutting;
using Books.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Books.Model.Entities;

namespace Books.Service
{
    public class BooksService : IBooksService
    {
        const string _bookCacheKey = "Books";
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public BooksService(IUOW UOW
                          , IMapper mapper
                          , ILogger logger
                          , IMemoryCache cache)
        {
            _uow = UOW;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<BookEntity>> GetBooksAsync()
        {
            if (_cache.TryGetValue(_bookCacheKey, out List<BookEntity> books))
            {
                return books;
            }

            books = await _uow.BooksRepository
                              .Query()
                              .ToListAsync();

            _cache.Set(_bookCacheKey, books);

            return books;
        }

        public async Task<int> GetTotalBooksAsync()
        {
            var bookList = await GetBooksAsync();

            return await Task.FromResult(bookList.Count);
        }

        public async Task<BookEntity> AddBookAsync(BookEntity book)
        {
            if (_cache.TryGetValue(_bookCacheKey, out List<BookEntity> books))
            {
                books.Add(book);
            }

            await _uow.BooksRepository.AddAsync(book);
            await _uow.CommitAsync();

            return book;
        }

        public async Task<bool> RemoveAsync(BookEntity book)
        {
            if (_cache.TryGetValue(_bookCacheKey, out List<BookEntity> books))
            {
                books.Remove(book);
            }

            _uow.BooksRepository.Remove(book);
            await _uow.CommitAsync();

            return true;
        }

        public async Task<bool> UpdateToReadStatusAsync(BookEntity book)
        {
            book.IsRead = true;

            if (_cache.TryGetValue(_bookCacheKey, out List<BookEntity> books))
            {
                books.FirstOrDefault(b => b.Id == book.Id)
                     .IsRead = true;
            }

            _uow.BooksRepository.Edit(book);
            await _uow.CommitAsync();

            return true;
        }
    }
}
