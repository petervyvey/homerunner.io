
using HomeRunner.Foundation.Logging;
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
			@"-----------------------------------------------------------------",
			@" _   _                      ____                                 ",
 			@"| | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __    ",
 			@"| |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__|   ",
 			@"|  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |      ",
 			@"|_| |_|\___/|_| |_| |_|\___|_|_\_\\__,_|_| |_|_| |_|\___|_|      ",
			@"        |  _ \ ___  __ _  __| |  \/  | ___   __| | ___| |        ",
			@"        | |_) / _ \/ _` |/ _` | |\/| |/ _ \ / _` |/ _ \ |        ",
			@"        |  _ <  __/ (_| | (_| | |  | | (_) | (_| |  __/ |        ",
			@"        |_| \_\___|\__,_|\__,_|_|  |_|\___/ \__,_|\___|_|        ",
            @"                                                                 ",                                                 
			@"-----------------------------------------------------------------"
			};

		private const string BASE_URI_TEMPLATE = "http://{0}:{1}";
		private const string HOST_ADDRESS_CONFIGURATION_KEY = "host.address";
		private const string HOST_PORT_CONFIGURATION_KEY = "host.port";

		private static void Main(string[] args)
		{
			Console.Clear();

			HEADER.ToList().ForEach(x => Console.WriteLine(x));
			Console.WriteLine(string.Format (typeof(Program).FullName));
			Console.WriteLine("Press q to quit ...");
			Console.WriteLine("-----------------------------------------------------------------");


			XmlConfigurator.Configure();

			try
			{
				var address = ConfigurationManager.AppSettings[HOST_ADDRESS_CONFIGURATION_KEY];
				var port = ConfigurationManager.AppSettings[HOST_PORT_CONFIGURATION_KEY];
				int _port = !string.IsNullOrEmpty(port) ? int.Parse(port) : 8000;

				Logger.Log.Info(string.Format("Network binding: {0}", string.Format(BASE_URI_TEMPLATE, address, _port)));
				using (WebApp.Start<Startup>(new StartOptions(string.Format(BASE_URI_TEMPLATE, address, _port))))
				{
					while (true)
					{
						while (!Console.KeyAvailable)
						{
							Thread.Sleep(250);
						}

						if (Console.ReadKey().Key == ConsoleKey.Q) 
						{
							Console.WriteLine();
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message);
			}

		}
	}
}
