using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Books.WPF.ViewModels
{
    public class DelegateCommandAsync : ICommand
    {
        private readonly Func<object, Task> _executeParam = null;

        public DelegateCommandAsync(Func<object, Task> execute)
        {
            _executeParam = execute ?? throw new ArgumentNullException("execute is null.");
        }

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter) => await _executeParam.Invoke(parameter);

        public event EventHandler CanExecuteChanged;
    }
}
