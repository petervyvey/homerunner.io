
using System;
using CommandLine;

namespace HomeRunner.CommandLine
{
	public class Arguments
	{
		[Option('v', "verbose", DefaultValue = false, HelpText = "Verbose")]
		public bool Verbose { get; set; }
	}
}

