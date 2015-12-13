
using Autofac;
using HomeRunner.Foundation.Logging;
using log4net.Config;
using MassTransit;
using System;

namespace HomeRunner.Consumer.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

			IContainer container = Program.ConfigureAutofac ();
			IBusControl bus = Program.ConfigureMassTransit (container);
			var handle = Program.ConfigureConsumers (bus);

			Logger.Log.Info (string.Empty);
			Logger.Log.Info ("Press any key to quit ...");

            Console.ReadKey();

            handle.Disconnect();
			bus.Stop();
        }

		private static IContainer ConfigureAutofac()
		{
			IContainer container = AutofacConfig.BuildContainer();
			Logger.Log.Info ("Autofac configuration DONE");

			return container;
		}

		private static IBusControl ConfigureMassTransit(IContainer container)
		{
			IBusControl bus = MassTransitConfig.Configure(container);
			bus.Start();
			Logger.Log.Info ("MassTransit configuration DONE");

			return bus;
		}

		private static ConnectHandle ConfigureConsumers(IBusControl bus )
		{
			ConnectHandle handle = bus.ConnectConsumer<CommandMessageConsumer>();
			Logger.Log.Info ("Consumer configuration DONE");

			return handle;
		}
    }
}
