using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Books.Domain.Entities;

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

        public async Task<List<Book>> GetBooksAsync(User user, int from, int to)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var response = await client.GetAsync($"{_baseURI}/books?from={from}&to={to}");
                    if (response.IsSuccessStatusCode)
                    {
                        books = await response.Content.ReadAsAsync<List<Book>>();
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

        public async Task<Book> SaveBookAsync(User user, Book book)
        {
            Book bookToReturn = new Book();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var response = await client.PutAsJsonAsync($"{_baseURI}/books", book);
                    if (response.IsSuccessStatusCode)
                    {
                        bookToReturn = await response.Content.ReadAsAsync<Book>();
                    }
                }
            }
            catch (Exception)
            {
            }

            return bookToReturn;
        }

        public async Task<Book> NewBookAsync(User user, Book book)
        {
            Book bookToReturn = new Book();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                    var response = await client.PostAsJsonAsync($"{_baseURI}/books", book);
                    if (response.IsSuccessStatusCode)
                    {
                        bookToReturn = await response.Content.ReadAsAsync<Book>();
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
