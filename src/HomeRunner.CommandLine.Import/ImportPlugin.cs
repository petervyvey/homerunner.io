
using CommandLine;
using HomeRunner.CommandLine.Logging;
using HomeRunner.CommandLine.Utils;
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
            Arguments arguments = new Arguments();
            if (Parser.Default.ParseArguments(args, arguments))
            {
                Logger.Log.Info(string.Format("File: {0}", arguments.File));
                Logger.Log.Info(string.Format("Connection: {0}", arguments.Connection));
                Logger.Log.Info("Reading file");
                Logger.Log.Info("Start import");

                if (!arguments.Verbose)
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        ConsoleProgressBar.RenderConsoleProgress(i, '\u2590', ConsoleColor.DarkYellow, i.ToString() + "%");
                        Thread.Sleep(10);
                    }
                    Console.WriteLine("\r\n");
                }
                else
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        Logger.Log.Info(string.Format("Entity {0}", i));
                        Thread.Sleep(10);
                    }
                }

                Logger.Log.Info("File imported");
            }
        }
    }
}
