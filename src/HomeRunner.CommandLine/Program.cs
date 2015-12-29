
using HomeRunner.CommandLine.Logging;
using log4net.Config;
using System;
using System.Linq;

namespace HomeRunner.CommandLine
{
    class Program
    {
        private static readonly string[] HEADER = new[]
        {
            @"  _   _                      ____                              ",
            @" | | | | ___  _ __ ___   ___|  _ \ _   _ _ __  _ __   ___ _ __ ",
            @" | |_| |/ _ \| '_ ` _ \ / _ \ |_) | | | | '_ \| '_ \ / _ \ '__|",
            @" |  _  | (_) | | | | | |  __/  _ <| |_| | | | | | | |  __/ |   ",
            @" |_| |_|\___/|_| |_| |_|\___|_| \_\\__,_|_| |_|_| |_|\___|_|   ",
            @"                       / ___| |   |_ _|                        ",
            @"                      | |   | |    | |                         ",
            @"                      | |___| |___ | |                         ",
            @"                       \____|_____|___|                        ",
            @"                                                               "
        };

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Program.HEADER.ToList().ForEach(Console.WriteLine);
            Console.ResetColor();

            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(string.Format(typeof(Program).FullName));
            Console.WriteLine("Press q to quit ...");
            Console.WriteLine("-----------------------------------------------------------------");

            XmlConfigurator.Configure();

            try
            {
                PluginManager.StartPlugin(args[0], args);
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            finally
            {
                Logger.Log.Info("Closing CLI");
            }
        }

        private static void WriteMessage(string message)
        {
            if (Console.CursorLeft > 0) Console.Write("\r\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"), message);
            Console.ResetColor();
        }
    }
}
