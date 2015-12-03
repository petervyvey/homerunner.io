
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeRunner.Web.Host
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: string.Empty,
            //    defaults: new { controller = "Platform", action = "Index" }
            //);

            routes.MapMvcAttributeRoutes();
        }
    }
}