
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace HomeRunner.Web.Host.App_Start
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
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
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

            app.UseWebApi(config);

            //config.MessageHandlers.Add(new AuthorizatonTokenMessageHandler());
        }
    }
}