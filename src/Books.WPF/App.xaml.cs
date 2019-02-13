using Autofac;
using Books.CrossCutting;
using Books.WPF.Modules;
using Books.WPF.Services;
using Books.WPF.ViewModels;
using Books.WPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Books.WPF
{
    public partial class App : Application
    {
        private IContainer _container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var _mainWindows = new MainView();
            _container = BuildContainer(_mainWindows);
            var _mainVm = _container.Resolve<MainViewModel>() as MainViewModel;
            _mainWindows.DataContext = _mainVm;
            _mainWindows.Show();
        }

        static IContainer BuildContainer(MainView mainView)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ViewModelsModule>();
            builder.RegisterModule<LogModule>();
            builder.RegisterType<BackendService>().AsSelf();
            builder.Register<IBackendService>((c, p) => c.Resolve<BackendService>(new NamedParameter("requestURI", p.TypedAs<string>())));
            builder.RegisterInstance(mainView).As<IWindowManager>();

            return builder.Build();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            var logger = _container.Resolve<ILogger>();
            logger.Close();
        }
    }
}
