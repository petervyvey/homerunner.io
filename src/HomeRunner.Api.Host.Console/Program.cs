
using HomeRunner.Foundation.Logging;
using log4net.Config;
using Microsoft.Owin.Hosting;
using System.Configuration;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HomeRunner.Api.Host.Console
{
	internal class Program
	{
		private static readonly string[] HEADER = new[]
		{
			@"-----------------------------------------------------------------",
			@"    _   _                      ____                              ",
			@"   | | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __ ",
			@"   | |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__|",
			@"   |  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |   ",
			@"   |_| |_|\___/|_| |_|_|_|\___|_|_\_\\__,_|_| |_|_| |_|\___|_|   ",
			@"                / \  |  _ \_ _| | |__   ___  ___| |_             ",
			@"               / _ \ | |_) | |  | '_ \ / _ \/ __| __|            ",
			@"              / ___ \|  __/| |  | | | | (_) \__ \ |_             ",
			@"             /_/   \_\_|  |___| |_| |_|\___/|___/\__|            ",
			@"                                                                 ",
			@"-----------------------------------------------------------------"
			};

		private const string BASE_URI_TEMPLATE = "http://{0}:{1}";
		private const string HOST_ADDRESS_CONFIGURATION_KEY = "host.address";
		private const string HOST_PORT_CONFIGURATION_KEY = "host.port";

		private static void Main(string[] args)
		{
			System.Console.Clear();

			Program.HEADER.ToList().ForEach(x => System.Console.WriteLine(x));
			System.Console.WriteLine(string.Format (typeof(Program).FullName));
			System.Console.WriteLine("Press q to quit ...");
			System.Console.WriteLine("-----------------------------------------------------------------");


			XmlConfigurator.Configure();

			try
			{
				var address = ConfigurationManager.AppSettings[HOST_ADDRESS_CONFIGURATION_KEY];
				var port = ConfigurationManager.AppSettings[HOST_PORT_CONFIGURATION_KEY];
				int _port = !string.IsNullOrEmpty(port) ? int.Parse(port) : 8000;

				Logger.Log.Info(string.Format("Network binding: {0}", string.Format(Program.BASE_URI_TEMPLATE, address, _port)));
				using (WebApp.Start<Startup>(new StartOptions(string.Format(Program.BASE_URI_TEMPLATE, address, _port))))
				{
					while (true)
					{
						while (!System.Console.KeyAvailable)
						{
							Thread.Sleep(250);
						}

						if (System.Console.ReadKey().Key == ConsoleKey.Q) 
						{
							System.Console.WriteLine();
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
