using Autofac;
using Books.Domain.Entities;
using Books.WPF.Services;
using Books.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.WPF.Modules
{
    public enum ViewModelsType
    {
        EditBookViewModel,
        CardBookViewModel
    }

    public class ViewModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<ViewModelBase>((c, p) =>
            {
                var _type = p.TypedAs<ViewModelsType>();


                switch (_type)
                {
                    case ViewModelsType.EditBookViewModel:
                        var _model = p.TypedAs<Book>();
                        return c.Resolve<EditBookViewModel>(new NamedParameter("book", _model));
                    case ViewModelsType.CardBookViewModel:
                        _model = p.TypedAs<Book>();
                        var _service = p.TypedAs<IBackendService>();
                        return c.Resolve<CardBookViewModel>(new NamedParameter("book", _model), new NamedParameter("backendService", _service));
                    default:
                        return null;
                }
            });

            var _core = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(_core)
                   .Where(t => t.Name.EndsWith("ViewModel"));
        }
    }
}
