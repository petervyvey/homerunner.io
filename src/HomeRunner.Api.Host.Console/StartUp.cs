﻿
using Autofac;
using Autofac.Integration.WebApi;
using HalJsonNet;
using HalJsonNet.Serialization;
using HomeRunner.Api.Service;
using HomeRunner.Api.Service.Platform;
using HomeRunner.Foundation.Logging;
using Newtonsoft.Json;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using AutoMapper;
using System.Configuration;

namespace HomeRunner.Api.Host.Console
{
	internal sealed class Startup
	{
		internal readonly Type SERVICE = typeof(TaskActivityController);

		public void Configuration(IAppBuilder app)
		{
			try
			{
				Logger.Log.Info("Start configuration");

				var config = this.ConfigureHttp();
				var container = this.ConfigureAutofac(config);
				this.ConfigureAutoMapper();
				this.ConfigureOwin(app, config, container);
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Startup exception: {0}", ex.Message));
			}
		}

		private HttpConfiguration ConfigureHttp()
		{
			var urlBase = ConfigurationManager.AppSettings["hal.urlBase"];
			if(string.IsNullOrEmpty(urlBase)) throw new Exception ("HAL JSON URL base not configured");

			HttpConfiguration config = new HttpConfiguration();
			config.MapHttpAttributeRoutes();

			config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings = new JsonSerializerSettings
			{
				Formatting = Newtonsoft.Json.Formatting.Indented,
				ContractResolver = new JsonNetHalJsonContactResolver(new HalJsonConfiguration(urlBase)),
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
			Mapper.Initialize (config => { });
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

