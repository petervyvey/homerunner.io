
using CommandLine;
using HomeRunner.CommandLine.Utils;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Logging;
using System;
using System.Threading;

namespace HomeRunner.CommandLine.Import
{
    [Plugin("import")]
    public class ImportPlugin
        : Plugin
    {
        public override void Start(string[] args)
        {
			ImportArguments arguments = new ImportArguments();
            if (Parser.Default.ParseArguments(args, arguments))
            {
				if(arguments.Verbose)
				{
					Logger.Log.Info(string.Format("Verbose: {0}", arguments.Verbose));

				}

				Logger.Log.Info(string.Format("Connection: {0}", arguments.Connection));
				Logger.Log.Info(string.Format("Reading file: {0}", arguments.File));
                Logger.Log.Info("Start import");

                if (!arguments.Verbose)
                {
					Console.WriteLine("\r\n");
                    for (int i = 0; i <= 2000; i++)
                    {
                        ConsoleProgressBar.RenderConsoleProgress(i, 2000, ConsoleColor.Green, i.ToString());
                        Thread.Sleep(1);
                    }
					Console.WriteLine("\r\n");
                }
                else
                {
                    for (int i = 0; i <= 2000; i++)
                    {
                        Logger.Log.Info(string.Format("Entity {0}", i));
                        Thread.Sleep(1);
                    }
                }

                Logger.Log.Info("File imported");
            }
        }
    }
}
