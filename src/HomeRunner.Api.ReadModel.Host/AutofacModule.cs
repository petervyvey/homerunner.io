﻿
using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using MediatR;
using System.Collections.Generic;

namespace HomeRunner.Api.ReadModel.Host
{
	public class AutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterApiControllers(typeof(ReadModel.AutofacModule).Assembly);
			builder.RegisterAssemblyTypes(typeof (IMediator).Assembly).AsImplementedInterfaces();

			builder.Register<SingleInstanceFactory>(ctx =>
				{
					var c = ctx.Resolve<IComponentContext>();
					return t =>
					{
						object instance;
					    if (c.IsRegisteredWithKey("request-with-logging-read", t))
					    {
					        instance = c.ResolveKeyed("request-with-logging-read", t);
					    }
					    else
					    {
					        instance = c.Resolve(t);
					    }

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

