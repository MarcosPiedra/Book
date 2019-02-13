using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.WPF.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
        }

        protected DelegateCommandAsync CreateCommandAsync(Func<object, Task> execute) => new DelegateCommandAsync(execute);
        protected void OnPropertyChangedEmpty() => OnPropertyChanged(String.Empty);
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
        public Action OnCloseWindow { get; set; } = null;
    }
}
