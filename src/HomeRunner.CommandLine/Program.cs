
using log4net.Config;
using System;
using System.Linq;
using CommandLine;

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

		internal static log4net.ILog Logger = null;

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Program.HEADER.ToList().ForEach(Console.WriteLine);
            Console.ResetColor();

            Console.WriteLine("-----------------------------------------------------------------");

			XmlConfigurator.Configure();
			Program.Logger = log4net.LogManager.GetLogger(typeof(Program));
			Logger.Info("Configured");

			Arguments arguments = new Arguments();
			Parser.Default.ParseArguments(args, arguments);
			if (arguments.Verbose){
				Program.TurnOnLogging();
			}

            try
            {
                PluginManager.StartPlugin(args[0], args);
            }
            catch(Exception ex)
            {
				Logger.Error(ex.Message);
            }
            finally
            {
				Console.WriteLine("-----------------------------------------------------------------");
				Program.WriteMessage("Closing CLI");
            }
        }

		private static void TurnOnLogging()
		{
			log4net.Repository.ILoggerRepository[] repositories= log4net.LogManager.GetAllRepositories();

			//Configure all loggers to be at the debug level.
			foreach (log4net.Repository.ILoggerRepository repository in repositories)
			{
				repository.Threshold = repository.LevelMap["DEBUG"];
				log4net.Repository.Hierarchy.Hierarchy hier = (log4net.Repository.Hierarchy.Hierarchy)repository;
				log4net.Core.ILogger[] loggers=hier.GetCurrentLoggers();
				foreach (log4net.Core.ILogger logger in loggers)
				{
					((log4net.Repository.Hierarchy.Logger) logger).Level = hier.LevelMap["DEBUG"];
				}
			}

			//Configure the root logger.
			log4net.Repository.Hierarchy.Hierarchy h = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
			log4net.Repository.Hierarchy.Logger rootLogger = h.Root;
			rootLogger.Level = h.LevelMap["DEBUG"];

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
