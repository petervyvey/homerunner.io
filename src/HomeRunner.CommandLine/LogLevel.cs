
using log4net;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System;
using System.Linq;

namespace HomeRunner.CommandLine
{
    public static class LogLevel
    {
        internal static void SetLevel(string level)
        {
            level = (new string[] { "NONE", "DEBUG", "INFO", "WARN", "ERROR", "FATAL" })
                .SingleOrDefault(x => x.Equals(level, StringComparison.InvariantCultureIgnoreCase)) 
                ?? 
                Arguments.DEFAULT_LOG_LEVEL;

            Program.WriteFormat("{0} {1}", "log level", level.ToUpper());

            ILoggerRepository[] repositories = LogManager.GetAllRepositories();

            // Configure all loggers to be at the debug level.
            foreach (ILoggerRepository repository in repositories)
            {
                repository.Threshold = repository.LevelMap[level];
                Hierarchy hierarchy = (Hierarchy)repository;
                ILogger[] loggers = hierarchy.GetCurrentLoggers();
                foreach (ILogger logger in loggers)
                {
                    ((Logger)logger).Level = hierarchy.LevelMap[level];
                }
            }

            // Configure the root logger.
            Hierarchy rootHierarchy = (Hierarchy)LogManager.GetRepository();
            Logger rootLogger = rootHierarchy.Root;
            rootLogger.Level = rootHierarchy.LevelMap[level];

        }
    }
}
