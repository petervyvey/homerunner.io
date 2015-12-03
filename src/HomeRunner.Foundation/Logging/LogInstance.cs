
namespace HomeRunner.Foundation.Logging
{
    internal class LogInstance
    {
        internal static readonly ILog Log = LogProvider.For<LogInstance>();
    }
}
