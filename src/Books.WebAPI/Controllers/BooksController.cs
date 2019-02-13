using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Books.CrossCutting;
using Books.Model;
using Books.Model.DTOs;
using Books.Model.Entities;
using Books.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.WebApi.Controllers
{
    [Authorize]
    [Route("api/v1")]
    public class BooksController : Controller
    {
        IMapper _mapper;
        ILogger _logger;
        IUserService _userService;
        IBooksService _booksServices;

        public BooksController(IUserService userService
                             , IBooksService booksServices
                             , IMapper mapper
                             , ILogger logger)
        {
            _userService = userService;
            _booksServices = booksServices;
            _mapper = mapper;
            _logger = logger;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        [Route("books")]
        public async Task<IActionResult> GetBooksAsync(int from = 0, int to = 0)
        {
            List<BookEntity> books = null;
            try
            {
                books = await _booksServices.GetBooksAsync();
            }
            catch (Exception _ex)
            {
                _logger.Write(_ex);

                return NoContent();
            }

            if (from >= 0
                && to > 0
                && from < to)
            {
                books = books.Skip(from)
                             .Take(to)
                             .ToList();
            }

            return Json(_mapper.Map<List<BookEntity>, List<BookDTO>>(books));
        }

        [HttpPost]
        [Route("books")]
        public async Task<IActionResult> AddtBooksAsync([FromBody] BookDTO bookDTO)
        {
            var bookToAdd = _mapper.Map<BookDTO, BookEntity>(bookDTO);
            bookToAdd.Id = null;
            try
            {
                await _booksServices.AddBookAsync(bookToAdd);
            }
            catch (Exception _ex)
            {
                _logger.Write(_ex);

                return NoContent();
            }

            return Json(_mapper.Map<BookEntity, BookDTO>(bookToAdd));
        }

        [HttpPut]
        [Route("books")]
        public async Task<IActionResult> UpdateBooksAsync([FromBody] BookDTO bookDTO)
        {
            var bookToAdd = _mapper.Map<BookDTO, BookEntity>(bookDTO);
            try
            {
                await _booksServices.UpdateToReadStatusAsync(bookToAdd);
            }
            catch (Exception _ex)
            {
                _logger.Write(_ex);

                return NoContent();
            }

            return Json(_mapper.Map<BookEntity, BookDTO>(bookToAdd));
        }

        [HttpDelete]
        [Route("books")]
        public async Task<IActionResult> DeleteBooksAsync([FromBody] BookDTO bookDTO)
        {
            var bookToAdd = _mapper.Map<BookDTO, BookEntity>(bookDTO);
            try
            {
                await _booksServices.RemoveAsync(bookToAdd);
            }
            catch (Exception _ex)
            {
                _logger.Write(_ex);

                return NoContent();
            }

            return Ok();
        }

        [HttpGet]
        [Route("total")]
        public async Task<IActionResult> GetTotalBooksAsync()
        {
            int _totalBooks = 0;
            try
            {
                _totalBooks = await _booksServices.GetTotalBooksAsync();
            }
            catch (Exception _ex)
            {
                _logger.Write(_ex);
                return NoContent();
            }

            return Json(_totalBooks);
        }
    }
}