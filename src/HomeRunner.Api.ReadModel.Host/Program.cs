
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using log4net.Config;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace HomeRunner.Api.ReadModel.Host
{
	internal class Program
	{
		private static readonly string[] HEADER = new[]
		{
			@" _   _                      ____                                 ",
 			@"| | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __    ",
 			@"| |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__|   ",
 			@"|  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |      ",
 			@"|_| |_|\___/|_| |_| |_|\___|_|_\_\\__,_|_| |_|_| |_|\___|_|      ",
			@"        |  _ \ ___  __ _  __| |  \/  | ___   __| | ___| |        ",
			@"        | |_) / _ \/ _` |/ _` | |\/| |/ _ \ / _` |/ _ \ |        ",
			@"        |  _ <  __/ (_| | (_| | |  | | (_) | (_| |  __/ |        ",
			@"        |_| \_\___|\__,_|\__,_|_|  |_|\___/ \__,_|\___|_|        ",
            @"                                                                 "
			};

		private const string BASE_URI_TEMPLATE = "http://{0}:{1}";
		private const string HOST_ADDRESS_CONFIGURATION_KEY = "host.address";
		private const string HOST_PORT_CONFIGURATION_KEY = "host.port";

		private static void Main(string[] args)
		{
			Console.Clear();
            Console.WriteLine("-----------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
			HEADER.ToList().ForEach(Console.WriteLine);
            Console.ResetColor();

            Console.WriteLine("-----------------------------------------------------------------");
			Console.WriteLine(string.Format (typeof(Program).FullName));
			Console.WriteLine("Press q to quit ...");
			Console.WriteLine("-----------------------------------------------------------------");

			XmlConfigurator.Configure();

			try
			{
				var address = ConfigurationManager.AppSettings[HOST_ADDRESS_CONFIGURATION_KEY];
				var port = ConfigurationManager.AppSettings[HOST_PORT_CONFIGURATION_KEY];
				int _port = !string.IsNullOrEmpty(port) ? int.Parse(port) : 8000;

                Program.WriteMessage(string.Format("Network binding: {0}", string.Format(BASE_URI_TEMPLATE, address, _port)));
				using (WebApp.Start<Startup>(new StartOptions(string.Format(BASE_URI_TEMPLATE, address, _port))))
				{
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
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.ToJson());
			    Logger.Log.Error(ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                Console.ReadKey();
			}

		}

        internal static void WriteMessage(string message)
        {
            if (Console.CursorLeft > 0) Console.Write("\r\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"), message);
            Console.ResetColor();
        }
	}
}
