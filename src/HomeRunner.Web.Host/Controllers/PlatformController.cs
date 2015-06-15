
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HomeRunner.Web.Host.Controllers
{
    
    public class PlatformController : Controller
    {
        public ActionResult Index()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Claims");
            }
            else
            {
                return this.View();
            }
        }

        [Authorize]
        public ActionResult Claims()
        {
            return this.View();
        }

        public ActionResult SignOut()
        {
            this.Request.GetOwinContext().Authentication.SignOut();

            return this.RedirectToAction("Index");
        }
    }
}
