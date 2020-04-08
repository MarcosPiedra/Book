using Books.CrossCutting;
using Books.Domain.Entities;
using Books.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Books.WPF.ViewModels
{
    public class CardBookViewModel : ViewModelBase
    {
        private readonly IBackendService backendService;

        public CardBookViewModel(Book book,
                                 IBackendService backendService)
        {
            Book = book;
            CheckAsReadCommand = CreateCommandAsync(CheckAsReadAsync);
            this.backendService = backendService;
        }

        private async Task CheckAsReadAsync(object arg) => await backendService.SaveBookAsync(User, Book);
        public User User { get; set; }
        public Book Book { get; set; }
        public bool IsReadOnly { get; } = true;
        public bool IsEnable { get; } = false;
        public ICommand CheckAsReadCommand { get; set; }
    }
}
