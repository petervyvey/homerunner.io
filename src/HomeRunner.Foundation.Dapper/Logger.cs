
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Foundation.Dapper
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
