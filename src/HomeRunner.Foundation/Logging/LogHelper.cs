
using Newtonsoft.Json;

namespace HomeRunner.Foundation.Logging
{
    public static class LogHelper
    {
        public static string BuildMessage(params object[] parts)
        {
            return JsonConvert.SerializeObject(parts);
        }
    }
}
