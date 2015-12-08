
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
			var port = ConfigurationManager.AppSettings.Get(HOST_PORT_CONFIGURATION_KEY);
			int _port = !string.IsNullOrEmpty(port) ? int.Parse(port) : 8000;

			System.Console.WriteLine("OWIN Web APi self host");
			using (WebApp.Start<Startup>(new StartOptions(string.Format(Program.BASE_URI_TEMPLATE, _port))))
			{
				System.Console.WriteLine("Press any key to quit ...");
				System.Console.ReadKey();
			}
		}
	}
}
