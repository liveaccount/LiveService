namespace NancyApp
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using System;
    using System.Threading.Tasks;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<AppHarborMiddleware>();
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = false,
            });

            //app.UseNancy();
        }

        public class AppHarborMiddleware : OwinMiddleware
        {
            public AppHarborMiddleware(OwinMiddleware next)
                : base(next)
            {
            }

            public override Task Invoke(IOwinContext context)
            {
                if (string.Equals(context.Request.Headers["X-Forwarded-Proto"], "https", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Request.Scheme = "https";
                }

                var forwardedForHeader = context.Request.Headers["X-Forwarded-For"];
                if (!string.IsNullOrEmpty(forwardedForHeader))
                {
                    context.Request.RemoteIpAddress = forwardedForHeader;
                }
                return Next.Invoke(context);
            }
        }
    }
}