
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Api.Host
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
