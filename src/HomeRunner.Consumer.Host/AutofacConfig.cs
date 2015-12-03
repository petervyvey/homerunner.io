using Autofac;

namespace HomeRunner.Consumer.Host
{
    public class AutofacConfig
    {
        public static IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule<AutofacModule>();
            builder.RegisterAssemblyModules(typeof(Domain.Service.AutofacModule).Assembly);
            builder.RegisterAssemblyModules(typeof(Foundation.NHibernate.AutofacModule).Assembly);

            return builder.Build();
        }
    }
}