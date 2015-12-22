
using HomeRunner.Api.ReadModel;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HomeRunner.Api.Host.StartUp))]

namespace HomeRunner.Api.Host
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            WebApiConfig.Configuration(app);
            AutoMapperConfig.Config();
        }
    }
}
