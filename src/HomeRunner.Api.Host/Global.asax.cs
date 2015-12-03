
using HomeRunner.Foundation.Logging;
using log4net.Config;
using System;
using System.Web;

namespace HomeRunner.Api.Host
{
    public class Global : HttpApplication
    {
        internal static readonly ILog Log = LogProvider.For<Global>();

        protected void Application_Start(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();

            Global.Log.Info("Web API service started.");
        }
    }
}