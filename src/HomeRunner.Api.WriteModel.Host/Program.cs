
using HomeRunner.Foundation.Configuration;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using log4net.Config;
using Microsoft.Owin.Hosting;
using System;
using System.Linq;
using System.Threading;

namespace HomeRunner.Api.WriteModel.Host
{
    internal class Program
	{
		private static readonly string[] HEADER = new[]
		{
			@"  _   _                      ____                                ",
			@" | | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __   ",
			@" | |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__|  ",
			@" |  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |     ",
			@" |_| |_|\___/|_|_|_| |_|\___|_| \_\\__,_|_| |_|_|_|_|\___|_|     ",
			@"      \ \      / / __(_) |_ ___|  \/  | ___   __| | ___| |       ",
			@"       \ \ /\ / / '__| | __/ _ \ |\/| |/ _ \ / _` |/ _ \ |       ",
			@"        \ V  V /| |  | | ||  __/ |  | | (_) | (_| |  __/ |       ",
			@"         \_/\_/ |_|  |_|\__\___|_|  |_|\___/ \__,_|\___|_|       ",
			@"                                                                 ",
			@"                                                                 "
			};

		private static void Main(string[] args)
		{
            Console.Clear();
            Console.WriteLine("-----------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            HEADER.ToList().ForEach(Console.WriteLine);
            Console.ResetColor();

            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(string.Format(typeof(Program).FullName));
            Console.WriteLine("Press q to quit ...");
            Console.WriteLine("-----------------------------------------------------------------");

            XmlConfigurator.Configure();

			try
			{
                var hostUri = WebApi.HostUri;
                Program.WriteMessage(string.Format("Network binding: {0}", hostUri.ToString()));

                using (WebApp.Start<Startup>(new StartOptions(hostUri.ToString())))
				{
					while (true)
					{
                        Program.WriteMessage("Listening ...");
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
