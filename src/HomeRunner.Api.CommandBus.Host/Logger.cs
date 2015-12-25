
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Api.CommandBus.Host
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
