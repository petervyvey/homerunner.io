
using Autofac;
using HomeRunner.Foundation.Logging;
using log4net.Config;
using MassTransit;
using System;
using System.Linq;

namespace HomeRunner.Consumer.Host
{
    class Program
    {
        static void Main(string[] args)
        {
			System.Console.Clear();

			var header = new[]
			{
				@"-----------------------------------------------------------------------",
				@"      _   _                      ____                               ",
				@"     | | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __  ",
				@"     | |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__| ",
				@"     |  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |    ",
				@"     |_| |_|\___/|_| |_| |_|\___|_| \_\\__,_|_| |_|_| |_|\___|_|    ",
				@"     ___ ___  _ __ ___  _ __ ___   __ _ _ __   __| |  _____   _____ ",
				@"    / __/ _ \| '_ ` _ \| '_ ` _ \ / _` | '_ \ / _` | / __\ \ / / __|",
				@"   | (_| (_) | | | | | | | | | | | (_| | | | | (_| | \__ \\ V / (__ ",
				@"    \___\___/|_| |_| |_|_| |_| |_|\__,_|_| |_|\__,_| |___/ \_/ \___|",
				@"                                                                    ",
				@"-----------------------------------------------------------------------",
				@"                                                                                     "
			};

			header.ToList().ForEach(x => System.Console.WriteLine(x));

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
