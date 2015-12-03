
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeRunner.Web.Host.Controllers
{

    public class PlatformController : Controller
    {
        [Route("")]
        public ActionResult Guest()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("StartTenant", new { tenantCode = "demo" });
            }
            else
            {
                return this.View();
            }
        }

        [Authorize, Route("{tenantCode}")]
        public ActionResult StartTenant(string tenantCode)
        {
            return RedirectToAction("StartApp", new { tenantCode = tenantCode, applicationCode = "platform" });
        }

        [Authorize, Route("{tenantCode}/{applicationCode}")]
        public ActionResult StartApp(string tenantCode, string applicationCode)
        {
            if (string.IsNullOrEmpty(applicationCode))
            {
                return RedirectToAction("StartApp", new { tenantCode = tenantCode, applicationCode = "platform" });
            }

            //this.SetApplicationConfiguration();
            this.ViewBag.TenantCode = tenantCode;
            this.ViewBag.ApplicationCode = applicationCode;

            return View("Index");
        }

        [Authorize]
        public ActionResult RenderApp(string groepCode, string applicationName)
        {
            this.ViewBag.GroepCode = groepCode;
            this.ViewBag.ApplicationName = applicationName ?? "Home";

            return View("App");
        }

        [Authorize]
        public ActionResult RenderNavigation(string groepCode)
        {
            this.ViewBag.GroepCode = groepCode;

            return View("Navigation");
        }

        [Authorize, Route("signin")]
        public ActionResult SignIn()
        {
            return this.RedirectToAction("StartTenant", new {tenantCode = "peter"});
        }

        [Route("signout")]
        public ActionResult SignOut()
        {
            this.Request.GetOwinContext().Authentication.SignOut();

            return this.RedirectToAction("Guest");
        }

        public ActionResult AppUnavailable(string groepCode, string applicationName)
        {
            this.ViewBag.ApplicationName = applicationName;
            this.ViewBag.GroepCode = groepCode;

            return View();
        }
    }
}
