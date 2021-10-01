namespace TokanPages.Backend.Shared.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;

    [ExcludeFromCodeCoverage]
    public static class CorsHeaders
    {
        private const string ACCESS_CONTROL_ALLOW_ORIGIN = "Access-Control-Allow-Origin";

        private const string ACCESS_CONTROL_ALLOW_HEADERS = "Access-Control-Allow-Headers";
        
        private const string ACCESS_CONTROL_ALLOW_METHODS = "Access-Control-Allow-Methods";
        
        private const string ACCESS_CONTROL_ALLOW_CREDENTIALS = "Access-Control-Allow-Credentials";
        
        private const string ACCESS_CONTROL_MAX_AGE = "Access-Control-Max-Age";

        public static void Ensure(HttpContext AHttpContext)
        {
            var LGetAllowOrigin = AHttpContext.Response.Headers[ACCESS_CONTROL_ALLOW_ORIGIN];
            var LGetAllowHeaders = AHttpContext.Response.Headers[ACCESS_CONTROL_ALLOW_HEADERS];
            var LGetAllowMethods = AHttpContext.Response.Headers[ACCESS_CONTROL_ALLOW_METHODS];
            var LGetAllowCredentials = AHttpContext.Response.Headers[ACCESS_CONTROL_ALLOW_CREDENTIALS];
            var LGetMaxAge = AHttpContext.Response.Headers[ACCESS_CONTROL_MAX_AGE];

            var LRequestOrigin = AHttpContext.Request.Headers["Origin"];

            const string SET_ALLOW_HEADERS = "Origin, X-Requested-With, Content-Type, Accept, Authorization";
            const string SET_ALLOW_METHODS = "GET, POST";
            const string SET_CREDENTIALS = "true";
            const string SET_MAX_AGE = "86400";

            if (LGetAllowOrigin.Count == 0)
                AHttpContext.Response.Headers.Add(ACCESS_CONTROL_ALLOW_ORIGIN, LRequestOrigin);

            if (LGetAllowHeaders.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(ACCESS_CONTROL_ALLOW_HEADERS, SET_ALLOW_HEADERS);

            if (LGetAllowMethods.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(ACCESS_CONTROL_ALLOW_METHODS, SET_ALLOW_METHODS);

            if (LGetAllowCredentials.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(ACCESS_CONTROL_ALLOW_CREDENTIALS, SET_CREDENTIALS);

            if (LGetMaxAge.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(ACCESS_CONTROL_MAX_AGE, SET_MAX_AGE);
        }
    }
}