
using HomeRunner.Foundation.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HomeRunner.Api.Host.Handler
{
    public class LoggingMessageHandler 
        : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Logger.Log.Info(string.Format("Handling API request: {0}", LogHelper.BuildMessage(request.RequestUri, request.Headers.Authorization)));
            try
            {
                return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
                {
                    HttpResponseMessage response = task.Result;
                    Logger.Log.Info(string.Format("Handled API request: {0}", LogHelper.BuildMessage(request.RequestUri, request.Headers.Authorization)));

                    response.Content.Headers.ContentType.MediaType = "application/hal+json";

                    return response;
                });
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString());

                Logger.Log.Debug(string.Format("HTTP response: {0}", response.StatusCode));

                return response;
            }
        }
    }
}