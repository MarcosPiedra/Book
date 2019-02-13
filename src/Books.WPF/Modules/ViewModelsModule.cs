using Autofac;
using Books.Model.Entities;
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
                var _model = p.TypedAs<BaseEntity>();

                switch (_type)
                {
                    case ViewModelsType.EditBookViewModel:
                        return c.Resolve<EditBookViewModel>(new NamedParameter("entityBase", _model));
                    case ViewModelsType.CardBookViewModel:
                        return c.Resolve<CardBookViewModel>(new NamedParameter("entityBase", _model));
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
