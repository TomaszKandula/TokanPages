using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Shared.Settings;

namespace TokanPages.Middleware
{

    public class CustomCors
    {

        private readonly RequestDelegate FRequestDelegate;

        public CustomCors(RequestDelegate ARequestDelegate) 
        {
            FRequestDelegate = ARequestDelegate;
        }

        public Task Invoke(HttpContext AHttpContext, AppUrls AAppUrls)
        {

            var LDevelopmentOrigin = AAppUrls.DevelopmentOrigin;
            var LDeploymentOrigin  = AAppUrls.DeploymentOrigin;
            var LRequestOrigin     = AHttpContext.Request.Headers["Origin"];

            if (LRequestOrigin == LDevelopmentOrigin || LRequestOrigin == LDeploymentOrigin)
            {

                AHttpContext.Response.Headers.Add("Access-Control-Allow-Origin", LRequestOrigin);
                AHttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                AHttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, PATCH, DELETE");
                AHttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                AHttpContext.Response.Headers.Add("Access-Control-Max-Age", "86400");

                if (AHttpContext.Request.Method == "OPTIONS") 
                {
                    AHttpContext.Response.StatusCode = 200;
                    return AHttpContext.Response.WriteAsync("OK");
                }

            }

            return FRequestDelegate(AHttpContext);

        }

    }

}
