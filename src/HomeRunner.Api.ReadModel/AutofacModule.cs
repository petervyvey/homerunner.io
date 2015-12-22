
using Autofac;
using Autofac.Features.Variance;
using MediatR;
using System.Collections.Generic;

namespace HomeRunner.Api.ReadModel
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
