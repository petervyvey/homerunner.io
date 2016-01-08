
using System.Web.Http;

namespace HomeRunner.Foundation.Web
{
    [RoutePrefix("$health")]
    public class HealthController
        : ApiController
    {
        [HttpGet, Route("")]
        public bool Get()
        {
            return true;
        }
    }
}
