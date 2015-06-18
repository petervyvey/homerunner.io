
using System.Threading.Tasks;
using HomeRunner.Web.Foundation;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.Client;

namespace HomeRunner.Web.Host.App_Start
{
    public static class AuthConfig
    {
        private const string CLIENT_ID = "homerunner";
        private const string CLIENT_SECRET = "secret";

        public static void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            //app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            //{
            //    MetadataAddress = Constant.IdentityServer.BASE_ADDRESS + "/wsfed/metadata",
            //    Wtrealm = "urn:homerunner",
            //    Wreply = "http://localhost:1000",

            //    Notifications = new WsFederationAuthenticationNotifications
            //    {
            //        SecurityTokenReceived = notification =>
            //        {
            //            var n = notification;
            //            return Task.FromResult(0);
            //        },

            //        SecurityTokenValidated = notification =>
            //        {
            //            var n = notification;
            //            return Task.FromResult(0);
            //        },
            //    },

            //    SignInAsAuthenticationType = "Cookies"
            //});

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = AuthConfig.CLIENT_ID,
                Authority = Constant.IdentityServer.BASE_ADDRESS,
                RedirectUri = "http://localhost:2360/",
                PostLogoutRedirectUri = "http://localhost:2360/",
                ResponseType = "code id_token token",
                //ResponseType = "id_token token",
                Scope = "openid email profile read write offline_access",
                //Scope = "email read write",

                SignInAsAuthenticationType = "Cookies",

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = async n =>
                    {
                        // filter "protocol" claims
                        List<Claim> claims = new List<Claim>(from c in n.AuthenticationTicket.Identity.Claims
                                                             where c.Type != "iss" &&
                                                                   c.Type != "aud" &&
                                                                   c.Type != "nbf" &&
                                                                   c.Type != "exp" &&
                                                                   c.Type != "iat" &&
                                                                   c.Type != "nonce" &&
                                                                   c.Type != "c_hash" &&
                                                                   c.Type != "at_hash"
                                                             select c);

                        // get userinfo data
                        UserInfoClient userInfoClient = new UserInfoClient(new Uri(Constant.Endpoint.OPENID_CONNECT_USER_INFO_ENDPOINT), n.ProtocolMessage.AccessToken);

                        UserInfoResponse userInfo = await userInfoClient.GetAsync();
                        userInfo.Claims.ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));

                        // get access and refresh token
                        OAuth2Client tokenClient = new OAuth2Client(new Uri(Constant.Endpoint.OPENID_CONNECT_TOKEN_ENDPOINT), AuthConfig.CLIENT_ID, AuthConfig.CLIENT_SECRET);
                        TokenResponse response = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);

                        claims.Add(new Claim("access_token", response.AccessToken));
                        claims.Add(new Claim("expires_at", DateTime.Now.AddSeconds(response.ExpiresIn).ToLocalTime().ToString()));
                        claims.Add(new Claim("refresh_token", response.RefreshToken));
                        claims.Add(new Claim("id_token", n.ProtocolMessage.IdToken));

                        n.AuthenticationTicket = new AuthenticationTicket(new ClaimsIdentity(claims.Distinct(new ClaimComparer()), n.AuthenticationTicket.Identity.AuthenticationType), n.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = async n =>
                    {
                        // if signing out, add the id_token_hint
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token").Value;
                            n.ProtocolMessage.IdTokenHint = idTokenHint;
                        }
                    },
                }
            });
        }
    }
}