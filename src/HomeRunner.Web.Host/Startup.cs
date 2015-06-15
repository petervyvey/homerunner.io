
using HomeRunner.Web.Host;
using HomeRunner.Web.Host.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace HomeRunner.Web.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            WebApiConfig.Configuration(app);
            AuthConfig.Configuration(app);
        }
    }
}
