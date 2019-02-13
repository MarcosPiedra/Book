using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Books.Model;
using Books.Model.DTOs;
using Books.Model.Entities;

namespace Books.WPF.Services
{
    public class BackendService : IBackendService
    {
        string _baseURI;

        public BackendService(string requestURI)
        {
            _baseURI = $"{requestURI.TrimEnd('/')}/api/v1";
        }

        public async Task<int> GetCountBooksAsync(User user)
        {
            int bookCounter = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var response = await client.GetAsync($"{_baseURI}/total");
                    if (response.IsSuccessStatusCode)
                    {
                        bookCounter = await response.Content.ReadAsAsync<int>();
                    }
                }
            }
            catch (Exception)
            {
            }

            return bookCounter;
        }

        public async Task<List<BookEntity>> GetBooksAsync(User user, int from, int to)
        {
            List<BookEntity> books = new List<BookEntity>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var response = await client.GetAsync($"{_baseURI}/books?from={from}&to={to}");
                    if (response.IsSuccessStatusCode)
                    {
                        books = await response.Content.ReadAsAsync<List<BookEntity>>();
                    }
                }
            }
            catch (Exception)
            {
            }

            return books;
        }

        public async Task<User> GetTokenAsync(string name, string password)
        {
            User user = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync($"{_baseURI}/authenticate", new User() { Password = password, Username = name });
                    if (response.IsSuccessStatusCode)
                    {
                        user = await response.Content.ReadAsAsync<User>();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return user;
        }

        public async Task<BookEntity> SaveBookAsync(User user, BookEntity book)
        {
            BookEntity bookToReturn = new BookEntity();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var response = await client.PutAsJsonAsync($"{_baseURI}/books", book);
                    if (response.IsSuccessStatusCode)
                    {
                        bookToReturn = await response.Content.ReadAsAsync<BookEntity>();
                    }
                }
            }
            catch (Exception)
            {
            }

            return bookToReturn;
        }

        public async Task<BookEntity> NewBookAsync(User user, BookEntity book)
        {
            BookEntity bookToReturn = new BookEntity();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var response = await client.PostAsJsonAsync($"{_baseURI}/books", book);
                    if (response.IsSuccessStatusCode)
                    {
                        bookToReturn = await response.Content.ReadAsAsync<BookEntity>();
                    }
                }
            }
            catch (Exception)
            {
            }

            return bookToReturn;
        }
    }
}
