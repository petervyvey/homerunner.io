
using CommandLine;
using HomeRunner.CommandLine.Utils;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Logging;
using System;
using System.Linq;
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
                Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, sessionId, "connection", arguments.Connection);
                Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, sessionId, "import file", arguments.File);
                Logger.Log.InfoFormat(Logger.CORRELATED_MESSAGE, sessionId, "start");

                if (!Logger.Log.IsDebugEnabled()) { Console.WriteLine(); }

                int counter = 0;
                Enumerable.Range(0, 100000).AsParallel().ForAll(x =>
                {
                    Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, sessionId, "entity", x);
                });

                //for (int i = 0; i <= 100000; i++)
                //{
                //    if (Logger.Log.IsDebugEnabled())
                //    {
                //        Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, sessionId, "entity", i);
                //    }
                //    else
                //    {
                //        ConsoleProgressBar.RenderConsoleProgress(i, 100000, ConsoleColor.Green, i.ToString());
                //    }
                //}

                Logger.Log.InfoFormat(Logger.MESSAGE, "done");
            }
        }
    }
}
