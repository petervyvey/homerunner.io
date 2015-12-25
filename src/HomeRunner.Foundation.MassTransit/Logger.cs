
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Foundation.MassTransit
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
