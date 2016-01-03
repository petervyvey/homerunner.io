
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
        public override void Start(string sessionId, string[] args)
        {
            this.SessionId = sessionId;

			ImportArguments arguments = new ImportArguments();
            if (Parser.Default.ParseArguments(args, arguments))
            {
                Logger.Log.InfoFormat(Logger.CONTENT, "connection", arguments.Connection);
                Logger.Log.InfoFormat(Logger.CONTENT, "import file", arguments.File);
                Logger.Log.InfoFormat(Logger.MESSAGE, "start");

                if (!Logger.Log.IsDebugEnabled()) { Console.WriteLine();  }

                    for (int i = 0; i <= 2000; i++)
                {
                    if (Logger.Log.IsDebugEnabled())
                    {
                        Logger.Log.InfoFormat(Logger.CONTENT, "entity", i);
                        Thread.Sleep(1);
                    }
                    else
                    {
                        ConsoleProgressBar.RenderConsoleProgress(i, 2000, ConsoleColor.Green, i.ToString());
                        Thread.Sleep(1);
                    }
                }

                Logger.Log.InfoFormat(Logger.MESSAGE, "done");
            }
        }
    }
}
