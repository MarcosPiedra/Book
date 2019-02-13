using Autofac;
using Books.CrossCutting;
using Books.WPF.Log;
using System.Collections.Generic;

namespace Books.WPF.Modules
{
    public class LogModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TextFileListener>();
            builder.Register<IEnumerable<ILoggerListener>>((c, p) =>
                new ILoggerListener[] { c.Resolve<TextFileListener>() }
            );
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
        }
    }
}
