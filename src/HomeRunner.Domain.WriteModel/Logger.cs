
using HomeRunner.Foundation.Logging;

namespace HomeRunner.Domain.WriteModel
{
    public class Logger
    {
        internal static readonly ILog Log = LogProvider.For<Logger>();
    }
}
