namespace TokanPages.WebApi.Middleware
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.Shared.Models;
    using Configuration;
    
    [ExcludeFromCodeCoverage]
    public class CustomCors
    {
        private readonly RequestDelegate FRequestDelegate;

        public CustomCors(RequestDelegate ARequestDelegate) 
            => FRequestDelegate = ARequestDelegate;

        public Task Invoke(HttpContext AHttpContext, ApplicationPaths AApplicationPaths)
        {
            var LDevelopmentOrigins = AApplicationPaths.DevelopmentOrigin.Split(';').ToList();
            var LDeploymentOrigins = AApplicationPaths.DeploymentOrigin.Split(';').ToList();
            var LRequestOrigin = AHttpContext.Request.Headers["Origin"];

            if (!LDevelopmentOrigins.Contains(LRequestOrigin) && !LDeploymentOrigins.Contains(LRequestOrigin))
                return FRequestDelegate(AHttpContext);
            
            CorsHeaders.Ensure(AHttpContext);

            // Necessary for pre-flight
            if (AHttpContext.Request.Method != "OPTIONS") 
                return FRequestDelegate(AHttpContext);
                
            AHttpContext.Response.StatusCode = 200;
            return AHttpContext.Response.WriteAsync("OK");
        }
    }
}