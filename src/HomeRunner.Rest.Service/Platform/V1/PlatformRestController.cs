
using HomeRunner.Rest.Service.Platform.V1.DataTransfer;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeRunner.Rest.Service.Platform.V1
{
    [RoutePrefix("api/v1/platform"), Authorize]
    public class PlatformRestController
        : ApiController
    {
        [HttpGet, Route("nodes")]
        public IEnumerable<Node> GetNodes()
        {
            return new List<Node>();
        }
    }
}
