
using Autofac.Integration.WebApi;
using HomeRunner.Foundation.ExceptionManagement;
using HomeRunner.Foundation.Logging;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace HomeRunner.Foundation.Web
{
    public class ApiExceptionFilterAttribute
        : ExceptionFilterAttribute, IAutofacExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string message;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            if (actionExecutedContext.Exception is BusinessException)
            {
                message = (actionExecutedContext.Exception as BusinessException).ToString();                
            }
            else if (actionExecutedContext.Exception is TechnicalException)
            {
                message = (actionExecutedContext.Exception as TechnicalException).ToString();
            }
            else if (actionExecutedContext.Exception is NotFoundException)
            {
                message = (actionExecutedContext.Exception as NotFoundException).ToString();
                statusCode = HttpStatusCode.NotFound;
            }
            else
            {
                message = actionExecutedContext.Exception.Message;
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(statusCode, message);
            Logger.Log.Error(message);
        }
    }
}
