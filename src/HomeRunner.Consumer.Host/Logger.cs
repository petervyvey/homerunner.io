
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Consumer.Host
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
