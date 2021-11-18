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
        private readonly RequestDelegate _requestDelegate;

        public CustomCors(RequestDelegate requestDelegate) 
            => _requestDelegate = requestDelegate;

        public Task Invoke(HttpContext httpContext, ApplicationPaths applicationPaths)
        {
            var developmentOrigins = applicationPaths.DevelopmentOrigin.Split(';').ToList();
            var deploymentOrigins = applicationPaths.DeploymentOrigin.Split(';').ToList();
            var requestOrigin = httpContext.Request.Headers["Origin"];

            if (!developmentOrigins.Contains(requestOrigin) && !deploymentOrigins.Contains(requestOrigin))
                return _requestDelegate(httpContext);
            
            CorsHeaders.Apply(httpContext);

            // Necessary for pre-flight
            if (httpContext.Request.Method != "OPTIONS") 
                return _requestDelegate(httpContext);
                
            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsync("OK");
        }
    }
}