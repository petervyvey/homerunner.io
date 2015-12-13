
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
		private const string BASE_URI_TEMPLATE = "http://{0}:{1}";
		private const string HOST_ADDRESS_CONFIGURATION_KEY = "host.address";
		private const string HOST_PORT_CONFIGURATION_KEY = "host.port";

		private static void Main(string[] args)
		{
			System.Console.Clear();

			var header = new[]
			{
				@"    _   _                      ____                              ",
				@"   | | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __ ",
				@"   | |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__|",
				@"   |  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |   ",
				@"   |_| |_|\___/|_| |_|_|_|\___|_|_\_\\__,_|_| |_|_| |_|\___|_|   ",
				@"                / \  |  _ \_ _| | |__   ___  ___| |_             ",
				@"               / _ \ | |_) | |  | '_ \ / _ \/ __| __|            ",
				@"              / ___ \|  __/| |  | | | | (_) \__ \ |_             ",
				@"             /_/   \_\_|  |___| |_| |_|\___/|___/\__|            ",
				@"                                                                 "
			};

			header.ToList().ForEach(x => System.Console.WriteLine(x));


			XmlConfigurator.Configure();
			Logger.Log.Info (string.Format (typeof(Program).FullName));

			var address = ConfigurationManager.AppSettings[HOST_ADDRESS_CONFIGURATION_KEY];
			var port = ConfigurationManager.AppSettings[HOST_PORT_CONFIGURATION_KEY];
			int _port = !string.IsNullOrEmpty(port) ? int.Parse(port) : 8000;

			Logger.Log.Info(string.Format("Host binding: {0}", string.Format(Program.BASE_URI_TEMPLATE, address, _port)));
			using (WebApp.Start<Startup>(new StartOptions(string.Format(Program.BASE_URI_TEMPLATE, address, _port))))
			{
				Logger.Log.Info (string.Empty);
				Logger.Log.Info ("Press any key to quit ...");

				System.Console.ReadKey ();
			}
		}
	}
}
