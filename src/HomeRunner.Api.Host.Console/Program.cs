
using HomeRunner.Foundation.Logging;
using log4net.Config;
using Microsoft.Owin.Hosting;
using System.Configuration;

namespace HomeRunner.Api.Host.Console
{
	internal class Program
	{
		private const string BASE_URI_TEMPLATE = "http://localhost:{0}";
		private const string HOST_PORT_CONFIGURATION_KEY = "host.port";

		private static void Main(string[] args)
		{
			XmlConfigurator.Configure();

			var port = ConfigurationManager.AppSettings.Get(HOST_PORT_CONFIGURATION_KEY);
			int _port = !string.IsNullOrEmpty(port) ? int.Parse(port) : 8000;

			Logger.Log.Info (string.Format (typeof(Program).FullName));
			Logger.Log.Info (string.Format("Listening on URI: {0}", string.Format(Program.BASE_URI_TEMPLATE, _port)));

			using (WebApp.Start<Startup>(new StartOptions(string.Format(Program.BASE_URI_TEMPLATE, _port))))
			{
				Logger.Log.Info (string.Empty);
				Logger.Log.Info ("Press any key to quit ...");
				System.Console.ReadKey();
			}
		}
	}
}
