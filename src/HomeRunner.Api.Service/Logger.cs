
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Api.Service
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
