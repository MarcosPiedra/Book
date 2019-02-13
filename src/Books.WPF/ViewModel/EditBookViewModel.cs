using Books.CrossCutting;
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
    public class EditBookViewModel : ViewModelBase
    {
        public EditBookViewModel(BaseEntity entityBase)
        {
            Book = entityBase as BookEntity ?? throw new ArgumentNullException("BookEntity is null.");
            AcceptCommand = CreateCommandAsync(AcceptAsync);
            CancelCommand = CreateCommandAsync(CancelAsync);
        }

        public BookEntity Book { get; set; }
        public ICommand AcceptCommand { get; }
        public ICommand CancelCommand { get; }
        public bool IsReadOnly { get; } = false;
        public bool IsEnable { get; } = true;
        public Func<BookEntity, Task> OnAcceptAsync;
        public Action OnCancel;

        private async Task AcceptAsync(object arg)
        {
            await OnAcceptAsync?.Invoke(Book);
            OnCloseWindow?.Invoke();
        }
        private async Task CancelAsync(object arg)
        {
            await Task.FromResult(0);
            OnCancel?.Invoke();
            OnCloseWindow?.Invoke();
        }
    }
}
