using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HomeRunner.Web.Host.App_Start
{
    public class AuthorizatonTokenMessageHandler : DelegatingHandler
    {
        private const string HTTP_HEADER_AUTHORIZATION_SCHEME = "Bearer";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization != null && authorization.Scheme == HTTP_HEADER_AUTHORIZATION_SCHEME && !string.IsNullOrWhiteSpace(authorization.Parameter))
            {
                try
                {
                    //ClaimsPrincipal principal = TokenManager.ValidateToken(authorization.Parameter);
                    //Thread.CurrentPrincipal = principal;

                    if (HttpContext.Current != null) HttpContext.Current.User = Thread.CurrentPrincipal;

                    return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
                    {
                        HttpResponseMessage response = task.Result;

                        return response;
                    });
                }
                catch (SecurityTokenValidationException)
                {
                    HttpResponseMessage response = new HttpResponseMessage
                    {
                        Content = new StringContent(HttpStatusCode.Unauthorized.ToString().ToLower()),
                        StatusCode = HttpStatusCode.Unauthorized
                    };

                    return response;
                }

            }
            else
            {
                return await Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    HttpResponseMessage response = new HttpResponseMessage
                    {
                        Content = new StringContent(HttpStatusCode.Unauthorized.ToString().ToLower()),
                        StatusCode = HttpStatusCode.BadRequest
                    };

                    return response;
                });
            }
        }
    }
}