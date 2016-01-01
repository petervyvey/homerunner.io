
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeRunner.Foundation.Web
{
    public class LoggingMessageHandler
        : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = string.Format("{0}{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);
            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, correlationId, "request", Logger.SerializeMessage(request.RequestUri, request.Headers.Authorization));
            try
            {
                var _request = await request.Content.ReadAsByteArrayAsync();
                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "content", Encoding.UTF8.GetString(_request));

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
                response.Content.Headers.ContentType.MediaType = "application/hal+json";

                Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, correlationId, "response", response.StatusCode);
                byte[] _response = response.IsSuccessStatusCode ? await response.Content.ReadAsByteArrayAsync() : Encoding.UTF8.GetBytes(response.ReasonPhrase);

                Logger.Log.DebugFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "response", Encoding.UTF8.GetString(_response));

                return response;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString());

                Logger.Log.DebugFormat(Logger.CORRELATED_CONTENT, correlationId, "statuscode", response.StatusCode);

                return response;
            }
            finally
            {
                Logger.Log.DebugFormat(Logger.CORRELATED_CONTENT, correlationId, "handled", Logger.SerializeMessage(request.RequestUri, request.Headers.Authorization));
            }
        }
    }
}
