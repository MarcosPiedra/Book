using Books.CrossCutting;
using Books.Domain.Entities;
using Books.WPF.Modules;
using Books.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Books.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        Func<string, IBackendService> _backendFactory;
        IBackendService _currentBackend = null;
        IWindowManager _windowManager = null;
        User _currentUser = null;
        ILogger _logger = null;
        Func<ViewModelsType, object, IBackendService, ViewModelBase> _viewModelFactory;
        bool _isConnected = false;
        string _loadText = "Load...";
        bool _firstSixLoaded = false;
        int _totalBooks = 0;
        int _booksLoaded = 0;
        bool _canConnect = true;

        public MainViewModel(Func<ViewModelsType, object, IBackendService, ViewModelBase> viewModelFactory
                           , Func<string, IBackendService> backendFactory
                           , IWindowManager windowManager
                           , ILogger logger)
        {
            _windowManager = windowManager;
            _backendFactory = backendFactory;
            _logger = logger;
            _viewModelFactory = viewModelFactory;
            ConnectCommand = CreateCommandAsync(ConnectAsync);
            NewBookCommand = CreateCommandAsync(NewBookAsync);
            LoadCommand = CreateCommandAsync(LoadAsync);
        }

        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }
        public bool CanConnect
        {
            get => _canConnect;
            set
            {
                if (_canConnect != value)
                {
                    _canConnect = value;
                    OnPropertyChanged("CanConnect");
                }
            }
        }
        public string LoadText
        {
            get => _loadText;
            set
            {
                if (_loadText != value)
                {
                    _loadText = value;
                    OnPropertyChanged("LoadText");
                }
            }
        }
        public string ServerURL { get; set; } = "";
        public ICommand ConnectCommand { get; set; } = null;
        public ICommand NewBookCommand { get; set; } = null;
        public ICommand LoadCommand { get; set; } = null;
        public ObservableCollection<CardBookViewModel> CardBooks { get; set; } = new ObservableCollection<CardBookViewModel>();

        private async Task ConnectAsync(object param)
        {
            if (!CanConnect)
            {
                _windowManager.Message("It is not possible to connect now");

                return;
            }

            if (IsConnected)
            {
                IsConnected = false;
            }
            User user = null;
            try
            {
                _currentBackend = _backendFactory.Invoke(param as string);
                user = await _currentBackend.GetTokenAsync("UserNameX", "P@assw0rd");
                if (user == null)
                {
                    _windowManager.Message("Could not connect :(");

                    return;
                }
            }
            catch (Exception _ex)
            {
                _logger.Write(_ex);

                return;
            }

            CardBooks.Clear();
            _firstSixLoaded = false;
            _totalBooks = await _currentBackend.GetCountBooksAsync(user);
            _booksLoaded = 0;
            _currentUser = user;
            IsConnected = true;
            LoadText = "Load...";
        }
        private async Task NewBookAsync(object param)
        {
            if (!IsConnected)
            {
                _windowManager.Message("Could not connect :(");

                return;
            }

            var entity = new Book() { PublicationDate = DateTime.Now, Id = 0 };
            var vm = _viewModelFactory.Invoke(ViewModelsType.EditBookViewModel, entity, _currentBackend) as EditBookViewModel;

            vm.OnAcceptAsync = async (book) =>
            {
                var newBook = await _currentBackend.NewBookAsync(_currentUser, entity);
                var cardVm = _viewModelFactory(ViewModelsType.CardBookViewModel, newBook, _currentBackend) as CardBookViewModel;
                cardVm.User = _currentUser;
                CardBooks.Add(cardVm);
                _totalBooks++;
            };

            _windowManager.OpenDialogView(ViewModelsType.EditBookViewModel, vm);

            await Task.FromResult(true);
        }
        private async Task LoadAsync(object arg)
        {
            if (!IsConnected)
            {
                _windowManager.Message("Could not connect :(");
                CardBooks.Clear();

                return;
            }

            if (_totalBooks == _booksLoaded)
            {
                _windowManager.Message("No books to load");

                return;
            }

            CanConnect = false;
            var booksToLoad = 0;
            var from = 0;

            if (_firstSixLoaded)
            {
                from = _booksLoaded;
                booksToLoad = _totalBooks;
            }
            else
            {
                booksToLoad = 6;
                CardBooks.Clear();
            }

            var books = await _currentBackend.GetBooksAsync(_currentUser, from, booksToLoad);
            books.ToList()
                 .ForEach(b =>
                 {
                     if (CardBooks.Count(cb => cb.Book.Id == b.Id) > 0)
                         return;

                     var _cardVm = _viewModelFactory(ViewModelsType.CardBookViewModel, b, _currentBackend) as CardBookViewModel;
                     _cardVm.User = _currentUser;
                     CardBooks.Add(_cardVm);
                 });

            _booksLoaded = booksToLoad;
            if (_totalBooks == _booksLoaded)
                LoadText = $"--";

            _firstSixLoaded = true;
            CanConnect = true;
        }
    }
}
