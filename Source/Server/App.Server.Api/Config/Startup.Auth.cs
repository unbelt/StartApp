namespace App.Server.Api.Config
{
    using System;

    using App.Data;
    using App.Server.Api.Providers;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;
    using Owin;

    public static partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(AppDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            if (OAuthOptions == null)
            {
                // Configure the application for OAuth based flow
                PublicClientId = "self";
                OAuthOptions = new OAuthAuthorizationServerOptions
                {
                    TokenEndpointPath = new PathString(Common.Constants.TokenEndpointPath),
                    Provider = new ApplicationOAuthProvider(PublicClientId),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                    // In production mode set AllowInsecureHttp = false
                    AllowInsecureHttp = true
                };
            }

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
