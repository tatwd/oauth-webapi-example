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

            // cors config
            app.UseCors(CorsOptions.AllowAll);

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


            // register routers
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            // `UseWebApi` must be enable while `Microsoft.AspNet.WebApi.Owin` was installed
            app.UseWebApi(config);
        }
    }
}
