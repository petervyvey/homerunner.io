
using Autofac;
using Autofac.Integration.WebApi;
using HalJsonNet;
using HalJsonNet.Serialization;
using HomeRunner.Api.Service;
using HomeRunner.Api.Service.Platform;
using Newtonsoft.Json;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace HomeRunner.Api.Host.Console
{
	internal sealed class Startup
	{
		internal Type service = typeof(TaskActivityController);

		public void Configuration(IAppBuilder app)
		{
            // HTTP config.
			HttpConfiguration config = new HttpConfiguration();
			config.MapHttpAttributeRoutes();

			config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings = new JsonSerializerSettings
			{
				Formatting = Newtonsoft.Json.Formatting.Indented,
				ContractResolver = new JsonNetHalJsonContactResolver(new HalJsonConfiguration("http://dev.homerunner.io/api")),
				NullValueHandling = NullValueHandling.Ignore
                //,TypeNameHandling = TypeNameHandling.Objects
			};

            // Autofac config.
			IContainer container = AutofacConfig.BuildContainer();
			AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
			config.DependencyResolver = resolver;

            // AutoMapper config.
            AutoMapperConfig.Config();

            // OWIN config.
			app.UseWebApi(config);
			app.UseAutofacMiddleware(container);
			//app.UseAutofacWebApi(GlobalConfiguration.Configuration);

			System.Console.WriteLine("OWIN configuration done");
		}
	}
}

