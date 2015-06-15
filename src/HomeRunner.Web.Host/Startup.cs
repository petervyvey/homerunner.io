
using HomeRunner.Web.Host;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;

//[assembly: OwinStartup(typeof(Startup))]

namespace HomeRunner.Web.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
			Debug.WriteLine(this);
        }
    }
}
