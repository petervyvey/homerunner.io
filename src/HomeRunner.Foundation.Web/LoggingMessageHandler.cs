
using System.Text;
using HomeRunner.Foundation.Logging;
using System;
using System.Net;
using System.Net.Http;
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
            Logger.Log.Info(string.Format("[{0}] Request:\r\n{1}", correlationId, LogHelper.BuildMessage(request.RequestUri, request.Headers.Authorization)));
            try
            {
                var requestMessage = await request.Content.ReadAsByteArrayAsync();
                Logger.Log.Info(string.Format("[{0}] Content:\r\n{1}", correlationId, Encoding.UTF8.GetString(requestMessage)));

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
                response.Content.Headers.ContentType.MediaType = "application/hal+json";

                Logger.Log.Info(string.Format("[{0}] Request handled", correlationId));
                byte[] _response = response.IsSuccessStatusCode ? await response.Content.ReadAsByteArrayAsync() : Encoding.UTF8.GetBytes(response.ReasonPhrase);

                Logger.Log.Debug(string.Format("[{0}] Response:\r\n{1}", correlationId, Encoding.UTF8.GetString(_response)));

                return response;
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
