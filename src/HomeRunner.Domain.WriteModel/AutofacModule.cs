
using Autofac;
using Autofac.Core;
using FluentValidation;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Logging;
using MassTransit;
using MassTransit.Log4NetIntegration;
using MediatR;
using System;
using System.Linq;

namespace HomeRunner.Domain.WriteModel
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .As(t => t.GetInterfaces()
                    .Where(i => i.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                    .Select(i =>
                        new KeyedService("request-handler-write", i)))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(UnitOfWorkDecorator<,>), typeof(IRequestHandler<,>), "request-handler-write")
                .Keyed("request-with-unit-of-work-write", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(ValidationDecorator<,>), typeof(IRequestHandler<,>), "request-with-unit-of-work-write")
                .Keyed("request-with-validation-write", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(LoggingDecorator<,>), typeof(IRequestHandler<,>), "request-with-validation-write")
                .Keyed("request-with-logging-write", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterType<LocalDomainEventPublisher>()
                .Named<IDomainEventMessagePublisher>("local-domain-event-publisher")
                .InstancePerDependency();

            builder
                .Register(c => new BusDomainEventPublisher(c.Resolve<IBus>(), c.ResolveNamed<IDomainEventMessagePublisher>("local-domain-event-publisher")))
                .As<IDomainEventMessagePublisher>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .SingleInstance();

			builder.Register<IDomainEntityValidatorProvider>(ctx => {
				IComponentContext c = ctx.Resolve<IComponentContext>();
				return new DomainEntityValidatorProvider(c);
			}).SingleInstance();

            //builder.RegisterDecorator<IDomainEventMessagePublisher>(
            //    (c, inner) =>
            //    {
            //        return new BusDomainEventPublisher(c.Resolve<IBus>(), inner);
            //    },
            //    "local-domain-event-publisher", "bus-domain-event-publisher")
            //    .InstancePerDependency();
        }
    }
}
