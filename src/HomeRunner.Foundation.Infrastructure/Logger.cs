
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;

namespace HomeRunner.Foundation.Infrastructure
{
    public class Logger
    {
        public const string CORRELATED_MESSAGE = "[{0}] {1}";

        public const string CORRELATED_CONTENT = "[{0}] {1} {2}";

        public const string CORRELATED_LONG_CONTENT = "[{0}] {1}\r\n{2}";

        public static readonly ILog Log = LogProvider.For<Logger>();

        public static string SerializeMessage(params object[] parts)
        {
            return parts.ToJson();
        }
    }
}
