namespace TokanPages.WebApi.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;

    [ExcludeFromCodeCoverage]
    public static class CorsHeaders
    {
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";

        private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        
        private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        
        private const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";
        
        private const string AccessControlMaxAge = "Access-Control-Max-Age";

        public static void Ensure(HttpContext httpContext)
        {
            var getAllowOrigin = httpContext.Response.Headers[AccessControlAllowOrigin];
            var getAllowHeaders = httpContext.Response.Headers[AccessControlAllowHeaders];
            var getAllowMethods = httpContext.Response.Headers[AccessControlAllowMethods];
            var getAllowCredentials = httpContext.Response.Headers[AccessControlAllowCredentials];
            var getMaxAge = httpContext.Response.Headers[AccessControlMaxAge];

            var requestOrigin = httpContext.Request.Headers["Origin"];

            const string allowHeaders = "Origin, X-Requested-With, Content-Type, Accept, Authorization";
            const string allowMethods = "GET, PUT, POST, PATCH, DELETE";
            const string credentials = "true";
            const string maxAge = "86400";

            if (getAllowOrigin.Count == 0)
                httpContext.Response.Headers.Add(AccessControlAllowOrigin, requestOrigin);

            if (getAllowHeaders.Count == 0 && requestOrigin.Count != 0)
                httpContext.Response.Headers.Add(AccessControlAllowHeaders, allowHeaders);

            if (getAllowMethods.Count == 0 && requestOrigin.Count != 0)
                httpContext.Response.Headers.Add(AccessControlAllowMethods, allowMethods);

            if (getAllowCredentials.Count == 0 && requestOrigin.Count != 0)
                httpContext.Response.Headers.Add(AccessControlAllowCredentials, credentials);

            if (getMaxAge.Count == 0 && requestOrigin.Count != 0)
                httpContext.Response.Headers.Add(AccessControlMaxAge, maxAge);
        }
    }
}