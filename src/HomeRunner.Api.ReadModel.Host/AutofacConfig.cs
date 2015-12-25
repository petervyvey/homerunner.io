
using Autofac;

namespace HomeRunner.Api.ReadModel.Host
{
	public class AutofacConfig
	{
		public static IContainer BuildContainer()
		{
			ContainerBuilder builder = new ContainerBuilder();

			builder.RegisterModule<AutofacModule>();
			builder.RegisterAssemblyModules(typeof(Domain.ReadModel.AutofacModule).Assembly);

			return builder.Build();
		}
	}
}

