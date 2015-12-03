
using Autofac.Integration.WebApi;
using HomeRunner.Foundation.ExceptionManagement;
using HomeRunner.Foundation.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace HomeRunner.Foundation.Web
{
    public class ServiceExceptionFilter
        : ExceptionFilterAttribute, IAutofacExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string message = string.Empty;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            if (actionExecutedContext.Exception is BusinessException)
            {
                BusinessException ex = actionExecutedContext.Exception as BusinessException;
                message = ex.ToString();
                
            }
            else if (actionExecutedContext.Exception is TechnicalException)
            {
                TechnicalException ex = actionExecutedContext.Exception as TechnicalException;
                message = ex.ToString();
                
            }
            else
            {
                Exception ex = actionExecutedContext.Exception;
                message = ex.Message;
            }

            
            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(statusCode, message);
            Logger.Log.Error(message);
        }
    }
}
