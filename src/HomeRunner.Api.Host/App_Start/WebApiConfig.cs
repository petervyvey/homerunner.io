
using Autofac;
using Autofac.Integration.WebApi;
using HalJsonNet.Serialization;
using HomeRunner.Api.Host.Handler;
using HomeRunner.Foundation.Web;
using HomeRunner.Api.Service.Platform;
using Newtonsoft.Json;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace HomeRunner.Api.Host
{
    public static class WebApiConfig
    {
        public static void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Web API attribute routing.
            config.MapHttpAttributeRoutes();

            // Remove all XML formatters.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Configure JSON formatter.
            config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                //ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ContractResolver = new JsonNetHalJsonContactResolver(new HalJsonNet.HalJsonConfiguration("http://dev.homerunner.io/api")),
                NullValueHandling = NullValueHandling.Ignore
                //,TypeNameHandling = TypeNameHandling.Objects
            };

            //config.Formatters.Add(new JsonHalMediaTypeFormatter());
            //config.Formatters.OfType<JsonHalMediaTypeFormatter>().First().SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            //{
            //    Authority = "http://dev.homerunner.io/authorization/core",
            //    //Authority = "https://localhost:44333/core",
            //    //RequiredScopes = new[] { "write" }

            //});

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(TaskActivityController).Assembly).PropertiesAutowired();
            builder.Register(c => new ApiExceptionFilterAttribute()).AsWebApiExceptionFilterFor<ApiController>().SingleInstance();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterAssemblyModules(typeof(Domain.ReadModel.AutofacModule).Assembly);
            builder.RegisterAssemblyModules(typeof(Domain.WriteModel.AutofacModule).Assembly);
            builder.RegisterAssemblyModules(typeof(Foundation.NHibernate.AutofacModule).Assembly);
            builder.RegisterAssemblyModules(typeof(Api.Service.AutofacModule).Assembly);
            IContainer container = builder.Build();

            AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

            app.UseWebApi(config);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);

            config.MessageHandlers.Add(new LoggingMessageHandler());
        }
    }
}