using Books.CrossCutting;
using Books.Model;
using Books.Model.Entities;
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
        public CardBookViewModel(BaseEntity entityBase)
        {
            Book = entityBase as BookEntity ?? throw new ArgumentNullException("BookEntity is null.");
            CheckAsReadCommand = CreateCommandAsync(CheckAsReadAsync);
        }

        private async Task CheckAsReadAsync(object arg)
        {
            await BackendService.SaveBookAsync(User, Book);
        }
        public User User { get; set; }
        public IBackendService BackendService { get; set; }
        public BookEntity Book { get; set; }
        public bool IsReadOnly { get; } = true;
        public bool IsEnable { get; } = false;
        public ICommand CheckAsReadCommand { get; set; }
    }
}
