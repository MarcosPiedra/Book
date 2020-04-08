using Books.WPF.Modules;
using Books.WPF.Services;
using Books.WPF.ViewModels;
using System.Windows;

namespace Books.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window, IWindowManager
    {
        public MainView()
        {
            InitializeComponent();
        }

        public void Message(string msg) => MessageBox.Show(msg);

        public void OpenDialogView(ViewModelsType type, ViewModelBase viewModelBase)
        {
            switch (type)
            {
                case ViewModelsType.EditBookViewModel:

                    var window = new EditBookView
                    {
                        Owner = this,
                        DataContext = viewModelBase
                    };
                    viewModelBase.OnCloseWindow = () => window.Close();
                    window.ShowDialog();

                    break;
                default:
                    break;
            }
        }
    }
}
