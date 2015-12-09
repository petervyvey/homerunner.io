
using Autofac;
using Autofac.Integration.WebApi;
using HalJsonNet;
using HalJsonNet.Serialization;
using HomeRunner.Foundation.Logging;
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
			Logger.Log.Info ("Start configuration");

			var config = this.ConfigureHttp();
			var container = this.ConfigureAutofac(config);
			this.ConfigureAutoMapper();
			this.ConfigureOwin(app, config, container);
		}

		private HttpConfiguration ConfigureHttp()
		{
			HttpConfiguration config = new HttpConfiguration();
			config.MapHttpAttributeRoutes();

			config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings = new JsonSerializerSettings
			{
				Formatting = Newtonsoft.Json.Formatting.Indented,
				ContractResolver = new JsonNetHalJsonContactResolver(new HalJsonConfiguration("http://dev.homerunner.io/api")),
				NullValueHandling = NullValueHandling.Ignore
					//,TypeNameHandling = TypeNameHandling.Objects
			};

			Logger.Log.Info ("HTTP configuration DONE");

			return config;
		}

		private IContainer ConfigureAutofac(HttpConfiguration config)
		{
			IContainer container = AutofacConfig.BuildContainer();
			AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
			config.DependencyResolver = resolver;

			Logger.Log.Info ("Autofac configuration DONE");

			return container;
		}

		private void ConfigureAutoMapper()
		{
			AutoMapperConfig.Config();

			Logger.Log.Info ("AutoMapper configuration DONE");
		}

		private void ConfigureOwin(IAppBuilder app, HttpConfiguration config, IContainer container)
		{
			app.UseWebApi(config);
			app.UseAutofacMiddleware(container);
			//app.UseAutofacWebApi(GlobalConfiguration.Configuration);

			Logger.Log.Info ("OWIN configuration DONE");
		}
	}
}

