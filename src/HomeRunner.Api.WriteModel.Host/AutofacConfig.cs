
using Autofac;

namespace HomeRunner.Api.WriteModel.Host
{
	public class AutofacConfig
	{
		public static IContainer BuildContainer()
		{
			ContainerBuilder builder = new ContainerBuilder();

			builder.RegisterModule<AutofacModule>();
			builder.RegisterAssemblyModules(typeof(Domain.ReadModel.AutofacModule).Assembly);
			builder.RegisterAssemblyModules(typeof(Domain.WriteModel.AutofacModule).Assembly);
			builder.RegisterAssemblyModules(typeof(Foundation.NHibernate.AutofacModule).Assembly);

			return builder.Build();
		}
	}
}

