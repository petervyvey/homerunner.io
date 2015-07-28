﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Logging;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Host;
using Thinktecture.IdentityServer.Host.Config;
#if __MonoCS__
using Thinktecture.IdentityServer.Mono;
#endif
using Thinktecture.IdentityServer.WsFederation.Configuration;
using Thinktecture.IdentityServer.WsFederation.Models;
using Thinktecture.IdentityServer.WsFederation.Services;

[assembly: OwinStartup(typeof(Startup))]

namespace Thinktecture.IdentityServer.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());
            //LogProvider.SetCurrentLogProvider(new TraceSourceLogProvider());

            // uncomment to enable HSTS headers for the host
            // see: https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security
            //app.UseHsts();

            app.Map("/core", coreApp =>
            {
#if __MonoCS__
                coreApp.SetDataProtectionProvider(new MonoDataProtectionProvider(app.Properties["host.AppName"] as string));
#endif

                var factory = InMemoryFactory.Create(
                    users: Users.Get(),
                    clients: Clients.Get(),
                    scopes: Scopes.Get());

                factory.CustomGrantValidator =
                    new Registration<ICustomGrantValidator>(typeof (CustomGrantValidator));

                factory.ConfigureClientStoreCache();
                factory.ConfigureScopeStoreCache();
                factory.ConfigureUserServiceCache();

                var idsrvOptions = new IdentityServerOptions
                {
                    Factory = factory,
                    SigningCertificate = Cert.Load(),
                    SiteName = "Home Runner",
                    RequireSsl = false,

                    CorsPolicy = CorsPolicy.AllowAll,
                    PluginConfiguration = Startup.ConfigurePlugIns,
                    EnableWelcomePage = false,

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = Startup.ConfigureIdentityProviders,
                        RememberLastUsername = false,
                        EnablePostSignOutAutoRedirect = true,
                        EnableSignOutPrompt = true,
                        PostSignOutAutoRedirectDelay = 5,
                        LoginPageLinks = new List<LoginPageLink>
                        {
                            new LoginPageLink {Href = "http://dev.homerunner.io/privacy",Text = "Privacy"}
                        }
                    },

                    LoggingOptions = new LoggingOptions
                    {
                        //EnableHttpLogging = true, 
                        //EnableWebApiDiagnostics = true,
                        //IncludeSensitiveDataInLogs = true
                    },

                    EventsOptions = new EventsOptions
                    {
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true,
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true
                    }
                };

                coreApp.UseIdentityServer(idsrvOptions);
            });
        }

        private static void ConfigurePlugIns(IAppBuilder pluginApp, IdentityServerOptions options)
        {
            var factory = new WsFederationServiceFactory(options.Factory);

            // data sources for in-memory services
            factory.Register(new Registration<IEnumerable<RelyingParty>>(GetRelyingParties()));
            factory.RelyingPartyService = new Registration<IRelyingPartyService>(typeof (InMemoryRelyingPartyService));

            var wsFedOptions = new WsFederationPluginOptions
            {
                IdentityServerOptions = options,
                Factory = factory
            };

            pluginApp.UseWsFederationPlugin(wsFedOptions);
        }

        private static IEnumerable<RelyingParty> GetRelyingParties()
        {
            return
                new List<RelyingParty>
                {
                    new RelyingParty
                    {
                        Name = "HomeRunner",
                        Enabled = true,
                        Realm = "urn:homerunner",
                        ReplyUrl = "http://dev.homerunner.io/",
                        ClaimMappings = new Dictionary<string, string>
                        {
                            {"sub", Constants.ClaimTypes.Subject},
                            {"given_name", Constants.ClaimTypes.Name},
                            {"email", Constants.ClaimTypes.Email}
                        }
                    }
                };
        }

        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var google = new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                Caption = "Google",
                SignInAsAuthenticationType = signInAsType,

                ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
                ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
            };
            app.UseGoogleAuthentication(google);

            var fb = new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                Caption = "Facebook",
                SignInAsAuthenticationType = signInAsType,

                AppId = "676607329068058",
                AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
            };
            app.UseFacebookAuthentication(fb);

            var twitter = new TwitterAuthenticationOptions
            {
                AuthenticationType = "Twitter",
                Caption = "Twitter",
                SignInAsAuthenticationType = signInAsType,

                ConsumerKey = "N8r8w7PIepwtZZwtH066kMlmq",
                ConsumerSecret = "df15L2x6kNI50E4PYcHS0ImBQlcGIt6huET8gQN41VFpUCwNjM"
            };
            app.UseTwitterAuthentication(twitter);

            var adfs = new WsFederationAuthenticationOptions
            {
                AuthenticationType = "adfs",
                Caption = "ADFS",
                SignInAsAuthenticationType = signInAsType,

                MetadataAddress = "https://adfs.leastprivilege.vm/federationmetadata/2007-06/federationmetadata.xml",
                Wtrealm = "urn:idsrv3"
            };
            app.UseWsFederationAuthentication(adfs);

            var aad = new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "aad",
                Caption = "Azure AD",
                SignInAsAuthenticationType = signInAsType,

                Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705",
                ClientId = "65bbbda8-8b85-4c9d-81e9-1502330aacba",
                RedirectUri = "https://localhost:44333/core/aadcb"
            };

            app.UseOpenIdConnectAuthentication(aad);
        }
    }
}