
using HomeRunner.CommandLine.Logging;

namespace HomeRunner.CommandLine
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
