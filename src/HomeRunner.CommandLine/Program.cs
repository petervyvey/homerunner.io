
using log4net.Config;
using System;
using System.Linq;
using CommandLine;
using System.Threading;

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

        internal static Guid SESSION_ID = Guid.NewGuid();

		internal static log4net.ILog LOGGER = null;

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Program.HEADER.ToList().ForEach(Console.WriteLine);
            Console.ResetColor();

            Console.WriteLine("-----------------------------------------------------------------");

            Thread.CurrentThread.Name = Program.SESSION_ID.ToString();

            Arguments arguments = new Arguments();
            Parser.Default.ParseArguments(args, arguments);

            XmlConfigurator.Configure();
            Program.LOGGER = log4net.LogManager.GetLogger(typeof(Program));
            // Always log argument parsing.
            Program.SetLogLevel("INFO");
            Program.LOGGER.InfoFormat("{0} {1}", "log level", arguments.LogLevel.ToUpper());
            Program.SetLogLevel(arguments.LogLevel);

            try
            {
                if (args.Count() == 0) throw new ArgumentException("No arguments provided.");

                PluginManager.StartPlugin(args[0], args);
            }
            catch(Exception ex)
            {
                Program.LOGGER.Error(ex.Message);
            }
            finally
            {
				Console.WriteLine("-----------------------------------------------------------------");
				Program.WriteMessage("Closing CLI");
            }
        }

		private static void SetLogLevel(string level)
		{
            level = (new string[] { "NONE", "DEBUG", "INFO", "WARN", "ERROR", "FATAL" }).SingleOrDefault(x => x.Equals(level, StringComparison.InvariantCultureIgnoreCase)) ?? Arguments.DEFAULT_LOG_LEVEL;

			log4net.Repository.ILoggerRepository[] repositories= log4net.LogManager.GetAllRepositories();

			// Configure all loggers to be at the debug level.
			foreach (log4net.Repository.ILoggerRepository repository in repositories)
			{
				repository.Threshold = repository.LevelMap[level];
				log4net.Repository.Hierarchy.Hierarchy hier = (log4net.Repository.Hierarchy.Hierarchy)repository;
				log4net.Core.ILogger[] loggers=hier.GetCurrentLoggers();
				foreach (log4net.Core.ILogger logger in loggers)
				{
					((log4net.Repository.Hierarchy.Logger) logger).Level = hier.LevelMap[level];
				}
			}

			// Configure the root logger.
			log4net.Repository.Hierarchy.Hierarchy h = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
			log4net.Repository.Hierarchy.Logger rootLogger = h.Root;
			rootLogger.Level = h.LevelMap[level];

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
