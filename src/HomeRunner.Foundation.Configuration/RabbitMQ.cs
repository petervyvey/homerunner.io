
using System;
using System.Configuration;

namespace HomeRunner.Foundation.Configuration
{
    public static class RabbitMQ
    {
        public const string DEFAULT_VIRTUAL_HOST_URI_FORMAT = "rabbitmq://{0}/{1}";

        public const string DEFAULT_EXCHANGE_URI_FORMAT = "rabbitmq://{0}/{1}/{2}";

        public static Uri VirtualHostUri
        {
            get
            {
                string address = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_ADDRESS];
                string virtualHost = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_VIRTUAL_HOST];

                return new Uri(string.Format(RabbitMQ.DEFAULT_VIRTUAL_HOST_URI_FORMAT, address, virtualHost));
            }
        }

        public static Uri CommandExchangeUri
        {
            get
            {
                string address = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_ADDRESS];
                string virtualHost = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_VIRTUAL_HOST];
                string exchange = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_EXCHANGE_COMMAND];

                return new Uri(string.Format(RabbitMQ.DEFAULT_EXCHANGE_URI_FORMAT, address, virtualHost, exchange));
            }
        }

        public static Uri EventExchangeUri
        {
            get
            {
                string address = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_ADDRESS];
                string virtualHost = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_VIRTUAL_HOST];
                string exchange = ConfigurationManager.AppSettings[AppSetting.RABBITMQ_EXCHANGE_EVENT];

                return new Uri(string.Format(RabbitMQ.DEFAULT_EXCHANGE_URI_FORMAT, address, virtualHost, exchange));
            }
        }
    }
}
