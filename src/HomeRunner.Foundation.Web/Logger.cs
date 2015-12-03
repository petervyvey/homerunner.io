
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Foundation.Web
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
