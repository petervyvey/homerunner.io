
using System;
using Autofac;
using Autofac.Features.Variance;
using MassTransit;
using MediatR;
using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace HomeRunner.Api.CommandBus.Host
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof (IMediator).Assembly).AsImplementedInterfaces();
            builder.RegisterConsumers(Assembly.GetExecutingAssembly());

            builder.Register(context => Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri("rabbitmq://localhost/command/"), h =>
                {
                    h.Username("slidingapps");
                    h.Password("slidingapps");
                });

                config.ReceiveEndpoint("NormalPriority", epc => epc.LoadFrom(context));
            }))
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object instance = c.IsRegisteredWithKey("request-with-logging-write", t) ? c.ResolveKeyed("request-with-logging-write", t) : c.Resolve(t);

                    return instance;
                };
            });

            builder.Register<MultiInstanceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });
        }
    }
}