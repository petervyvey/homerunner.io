
using Autofac;
using MassTransit;
using MassTransit.Log4NetIntegration;
using System;

namespace HomeRunner.Api.WriteModel
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.UseLog4Net();
                config.UseJsonSerializer();
                config.Durable = true;

                var host = config.Host(new Uri("rabbitmq://localhost/command/"), h =>
                {
                    h.Username("slidingapps");
                    h.Password("slidingapps");
                });
            }))
                .SingleInstance()
                .As<IBus>();
        }
    }
}
