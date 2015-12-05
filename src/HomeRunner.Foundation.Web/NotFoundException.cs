
using System;
using System.Net;

namespace HomeRunner.Foundation.Web
{
    public class NotFoundException 
        : Exception
    {
        public override string ToString()
        {
            return HttpStatusCode.NotFound.ToString();
        }
    }
}
