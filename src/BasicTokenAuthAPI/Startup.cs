using System;
using System.Threading.Tasks;
using System.Web.Http;
using BasicTokenAuthAPI.OAuth;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(BasicTokenAuthAPI.Startup))]

namespace BasicTokenAuthAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888

            HttpConfiguration config = new HttpConfiguration();

            // Cors config
            app.UseCors(CorsOptions.AllowAll);

            // OAuth config
            // must before router register
            OAuthConfiguration(app);

            // Register routers
            WebApiConfig.Register(config);

            // `UseWebApi` must be enable while `Microsoft.AspNet.WebApi.Owin` was installed
            app.UseWebApi(config);
        }

        /// <summary>
        /// OAuth 配置
        /// </summary>
        /// <param name="app"></param>
        public void OAuthConfiguration(IAppBuilder app)
        {
            // init custom OAuth authorization server provider
            var provider = new BaseOAuthAuthorizationServerProvider();

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true, // for dev mode
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60), // 1 hours
                Provider = provider
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
