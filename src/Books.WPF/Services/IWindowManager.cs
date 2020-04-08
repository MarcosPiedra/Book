using Books.WPF.Modules;
using Books.WPF.ViewModels;

namespace Books.WPF.Services
{
    public interface IWindowManager
    {
        void OpenDialogView(ViewModelsType editBookViewModel, ViewModelBase viewModelBase);
        void Message(string msg);
    }
}