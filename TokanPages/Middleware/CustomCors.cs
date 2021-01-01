using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Shared.Cors;
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

                CorsHeaders.Ensure(AHttpContext);

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
