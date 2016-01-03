
using System;
using System.Configuration;

namespace HomeRunner.Foundation.Configuration
{
    public static class WebApi
    {
        public const string DEFAULT_HOST_URI_FORMAT = "http://{0}:{1}";

        public static Uri HostUri
        {
            get
            {
                string address = ConfigurationManager.AppSettings[AppSetting.HOST_ADDRESS];
                string port = ConfigurationManager.AppSettings[AppSetting.HOST_PORT];
                int _port = !string.IsNullOrEmpty(port) ? int.Parse(port) : 8000;

                return new Uri(string.Format(WebApi.DEFAULT_HOST_URI_FORMAT, address, _port));
            }
        }
    }
}
