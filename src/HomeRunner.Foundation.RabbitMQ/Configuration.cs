
using System;
using System.Configuration;

namespace HomeRunner.Foundation.RabbitMQ
{
    public static class Configuration
    {
        public const string DEFAULT_QUEUE_URI_FORMAT = "rabbitmq://{0}/{1}/{2}";

        public static Uri Exchange
        {
            get
            {
                string address = ConfigurationManager.AppSettings["rabbitmq.address"];
                string virtualHost = ConfigurationManager.AppSettings["rabbitmq.virtualHost"];
                string exchange = ConfigurationManager.AppSettings["rabbitmq.exchange"];

                return new Uri(string.Format(Configuration.DEFAULT_QUEUE_URI_FORMAT, address, virtualHost, exchange));
            }
        }
    }
}
