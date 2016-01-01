
using Autofac;
using Autofac.Core;
using FluentValidation;
using HomeRunner.Domain.ReadModel.Platform;
using HomeRunner.Foundation.Dapper;
using HomeRunner.Foundation.Dapper.Filter;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.Decorator;
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
                        new KeyedService("request-handler-read", i)))
                .InstancePerDependency();

            //builder.RegisterGenericDecorator(typeof(UnitOfWorkDecorator<,>), typeof(IRequestHandler<,>), "request-handler")
            //    .Keyed("request-with-unit-of-work", typeof(IRequestHandler<,>))
            //    .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(ValidationDecorator<,>), typeof(IRequestHandler<,>), "request-handler-read")
                .Keyed("request-with-validation-read", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(typeof(LoggingDecorator<,>), typeof(IRequestHandler<,>), "request-with-validation-read")
                .Keyed("request-with-logging-read", typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .SingleInstance();

            builder.RegisterType<Criteria>().As<ICriteria>().InstancePerDependency();
            builder.RegisterType<Criterion>().As<ICriterion>().InstancePerDependency();

            builder.Register<Foundation.Dapper.IQueryProvider>(ctx =>
            {
                IComponentContext c = ctx.Resolve<IComponentContext>();
                return new QueryProvider(c);
            }).InstancePerLifetimeScope();

            builder.RegisterType<MappingProvider>().As<IMappingProvider>().SingleInstance();
            builder.RegisterType<DapperDatabaseContext>().As<IDatabaseContext>().InstancePerDependency();

            builder.RegisterGeneric(typeof(Criteria<>)).As(typeof(ICriteria<>)).InstancePerDependency();
            builder.Register<CriteriaFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (ICriteria)c.Resolve(typeof(ICriteria<>).MakeGenericType(t));
            }).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Criterion<>)).As(typeof(ICriterion<>)).InstancePerDependency();
            builder.Register<CriterionFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (ICriterion)c.Resolve(typeof(ICriterion<>).MakeGenericType(t));
            }).InstancePerLifetimeScope();
        }
    }
}
