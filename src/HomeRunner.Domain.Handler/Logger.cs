
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Domain.Service
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
