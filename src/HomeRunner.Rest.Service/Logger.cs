
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Rest.Service
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
