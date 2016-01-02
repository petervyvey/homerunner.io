
using System;
using CommandLine;

namespace HomeRunner.CommandLine
{
	public class Arguments
	{
        public const string DEFAULT_LOG_LEVEL = "WARN";

        [Option('l', "loglevel", DefaultValue = Arguments.DEFAULT_LOG_LEVEL, HelpText = "Log level")]
        public string LogLevel { get; set; }
    }
}

