
using Autofac;
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Logging;
using log4net.Config;
using MassTransit;
using System;
using System.Linq;
using System.Threading;

namespace HomeRunner.Api.CommandBus.Host
{
    class Program
    {
        private static readonly string[] HEADER = new[]
        {
            @"     _   _                      ____                               ",
            @"    | | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __  ",
            @"    | |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__| ",
            @"    |  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |    ",
            @"   _|_| |_|\___/|_| |_| |_|\___|_| \_\\__,_|_| |_|_|_|_|\___|_|    ",
            @"  / ___|___  _ __ ___  _ __ ___   __ _ _ __   __| | __ ) _   _ ___ ",
            @" | |   / _ \| '_ ` _ \| '_ ` _ \ / _` | '_ \ / _` |  _ \| | | / __|",
            @" | |__| (_) | | | | | | | | | | | (_| | | | | (_| | |_) | |_| \__ \",
            @"  \____\___/|_| |_| |_|_| |_| |_|\__,_|_| |_|\__,_|____/ \__,_|___/",
            @"                                                                   ",
            @"                                                                    "
        };

        private static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            HEADER.ToList().ForEach(Console.WriteLine);
            Console.ResetColor();

            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(string.Format(typeof (Program).FullName));
            Console.WriteLine("Press q to quit ...");
            Console.WriteLine("-----------------------------------------------------------------");

            XmlConfigurator.Configure();

            IBusControl bus = null;
            ConnectHandle handle = null;
            try
            {
                IContainer container = Program.ConfigureAutofac();
                bus = Program.ConfigureMassTransit(container);
                handle = Program.ConfigureConsumers(bus);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToJson());
                Logger.Log.Error(ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                Console.ReadKey();
            }
            finally
            {
                Console.WriteLine("-----------------------------------------------------------------");
            }

            Program.WriteMessage("Listening ...");
            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(250);
                }

                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    Program.WriteMessage("Received 'q' to quit");
                    break;
                }
            }

            handle.Disconnect();
            bus.Stop();

        }

        internal static void WriteMessage(string message)
        {
            if (Console.CursorLeft > 0) Console.Write("\r\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"), message);
            Console.ResetColor();
        }

        private static IContainer ConfigureAutofac()
		{
			IContainer container = AutofacConfig.BuildContainer();
            Program.WriteMessage("Autofac configuration DONE");

			return container;
		}

		private static IBusControl ConfigureMassTransit(IContainer container)
		{
			IBusControl bus = MassTransitConfig.Configure(container);
			bus.Start();
            Program.WriteMessage("MassTransit configuration DONE");

			return bus;
		}

		private static ConnectHandle ConfigureConsumers(IBusControl bus )
		{
			ConnectHandle handle = bus.ConnectConsumer<CommandMessageConsumer>();
            Program.WriteMessage("Consumer configuration DONE");

			return handle;
		}
    }
}
