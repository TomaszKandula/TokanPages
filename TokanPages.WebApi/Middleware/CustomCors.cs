namespace TokanPages.WebApi.Middleware
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.Shared.Models;
    using Backend.Shared.Helpers;
    
    [ExcludeFromCodeCoverage]
    public class CustomCors
    {
        private readonly RequestDelegate FRequestDelegate;

        public CustomCors(RequestDelegate ARequestDelegate) 
            => FRequestDelegate = ARequestDelegate;

        public Task Invoke(HttpContext AHttpContext, ApplicationPathsModel AApplicationPathsModel)
        {
            var LDevelopmentOrigins = AApplicationPathsModel.DevelopmentOrigin.Split(';').ToList();
            var LDeploymentOrigins = AApplicationPathsModel.DeploymentOrigin.Split(';').ToList();
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