
using Autofac;
using HomeRunner.Foundation.MassTransit;
using HomeRunner.Foundation.MessageBus;
using MassTransit;
using MassTransit.Log4NetIntegration;
using System.Configuration;

namespace HomeRunner.Api.WriteModel
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MassTransitConnector>().As<IBusConnector>();

            builder.Register(context => Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.UseLog4Net();
                config.UseJsonSerializer();
                config.Durable = true;

                config.Host(Foundation.Configuration.RabbitMQ.VirtualHostUri, host =>
                {
                    host.Username(ConfigurationManager.AppSettings[Foundation.Configuration.AppSetting.RABBITMQ_USER]);
                    host.Password(ConfigurationManager.AppSettings[Foundation.Configuration.AppSetting.RABBITMQ_PASSWORD]);
                });
            }))
                .SingleInstance()
                .As<IBus>();
        }
    }
}
