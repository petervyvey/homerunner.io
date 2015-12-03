
using Autofac;
using Autofac.Features.Variance;
using MassTransit;
using MassTransit.Log4NetIntegration;
using MediatR;
using System;
using System.Collections.Generic;

namespace HomeRunner.Rest.Service
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

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof (IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object instance = c.IsRegisteredWithKey("request-with-logging", t) ? c.ResolveKeyed("request-with-logging", t) : c.Resolve(t);

                    return instance;
                };
            });

            builder.Register<MultiInstanceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => (IEnumerable<object>) c.Resolve(typeof (IEnumerable<>).MakeGenericType(t));
            });
        }
    }
}
