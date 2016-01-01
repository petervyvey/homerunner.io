
using Autofac;
using HomeRunner.Foundation.Infrastructure.Transaction;
using NHibernate;

namespace HomeRunner.Foundation.NHibernate
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<DatabaseQueryProvider>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<SessionFactory>().InstancePerDependency();
            builder.Register(c => c.Resolve<SessionFactory>().OpenSession()).As<ISession>().InstancePerDependency();
        }
    }
}
