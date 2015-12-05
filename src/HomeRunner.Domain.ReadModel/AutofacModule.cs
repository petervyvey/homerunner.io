using Autofac;
using Autofac.Core;
using FluentValidation;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Logging;
using MediatR;
using System.Linq;

namespace HomeRunner.Domain.ReadModel
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .As(t => t.GetInterfaces()
                    .Where(i => i.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                    .Select(i =>
                        new KeyedService("request-handler", i)))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(UnitOfWorkDecorator<,>), typeof(IRequestHandler<,>), "request-handler")
                .Keyed("request-with-unit-of-work", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(ValidationDecorator<,>), typeof(IRequestHandler<,>), "request-with-unit-of-work")
                .Keyed("request-with-validation", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(LoggingDecorator<,>), typeof(IRequestHandler<,>), "request-with-validation")
                .Keyed("request-with-logging", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .SingleInstance();
        }
    }
}
