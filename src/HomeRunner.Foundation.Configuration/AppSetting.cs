
namespace HomeRunner.Foundation.Configuration
{
    public static class AppSetting
    {
        //<add key = "host.address" value="localhost" />
        //<add key = "host.port" value="8081" />

        private const string HOST_BASE_URI_TEMPLATE = "http://{0}:{1}";

        public const string HOST_ADDRESS = "host.address";

        public const string HOST_PORT = "host.port";


        //<add key = "hal.urlBase" value="http://dev.homerunner.io/api" />

        public const string HAL_URL_BASE = "hal.urlBase";


        //<add key = "dapper.provider" value="MySql.Data.MySqlClient" />
        //<add key = "dapper.connectionstring" value="Server=xxx;Database=xxx;Uid=xxx;Pwd=xxx;" />

        public const string DAPPER_PROVIDER = "dapper.provider";

        public const string DAPPER_CONNECTIONSTRING = "dapper.connectionstring";


        //<add key = "nhibernate.dialect" value="MySql" />
        //<add key = "nhibernate.connectionstring" value="Server=xxx;Database=HomeAutomation_DEV;Uid=xxx;Pwd=xxx;" />
        //<add key = "nhibernate.mappingAssembly" value="HomeRunner.Domain.ReadModel" />

        public const string NHIBERNATE_DIALECT = "nhibernate.dialect";

        public const string NHIBERNATE_CONNECTIONSTRING = "nhibernate.connectionstring";

        public const string NHIBERNATE_MAPPING_ASSEMBLY = "nhibernate.mappingAssembly";


        //<add key = "rabbitmq.address" value="localhost" />
        //<add key = "rabbitmq.virtualHost" value="xxx" />
        //<add key = "rabbitmq.exchange.command" value="Command" />
        //<add key = "rabbitmq.exchange.domainEvent" value="DomainEvent" />
        //<add key = "rabbitmq.queue.command" value="Command" />
        //<add key = "rabbitmq.user" value="xxx" />
        //<add key = "rabbitmq.password" value="xxx" />

        public const string RABBITMQ_ADDRESS = "rabbitmq.address";

        public const string RABBITMQ_VIRTUAL_HOST = "rabbitmq.virtualHost";

        public const string RABBITMQ_EXCHANGE_COMMAND = "rabbitmq.exchange.command";

        public const string RABBITMQ_EXCHANGE_EVENT = "rabbitmq.exchange.domainEvent";

        public const string RABBITMQ_QUEUE_COMMAND = "rabbitmq.queue.command";

        public const string RABBITMQ_USER = "rabbitmq.user";

        public const string RABBITMQ_PASSWORD = "rabbitmq.password";
    }
}
