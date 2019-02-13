using Autofac;
using Books.Data.EntityFramework.Context;
using Books.UnitOfWork;

namespace Books.WebApi.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoryModule>();

            builder.RegisterType(typeof(BooksContext)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(UOW)).As(typeof(IUOW)).InstancePerLifetimeScope();
        }
    }
}
