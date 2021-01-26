using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Shared.Cors
{
    public static class CorsHeaders
    {
        public static string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        public static string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        public static string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        public static string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";
        public static string AccessControlMaxAge = "Access-Control-Max-Age";

        public static void Ensure(HttpContext AHttpContext)
        {
            var LGetAllowOrigin = AHttpContext.Response.Headers[AccessControlAllowOrigin];
            var LGetAllowHeaders = AHttpContext.Response.Headers[AccessControlAllowHeaders];
            var LGetAllowMethods = AHttpContext.Response.Headers[AccessControlAllowMethods];
            var LGetAllowCredentials = AHttpContext.Response.Headers[AccessControlAllowCredentials];
            var LGetMaxAge = AHttpContext.Response.Headers[AccessControlMaxAge];

            var LRequestOrigin = AHttpContext.Request.Headers["Origin"];

            const string LSetAllowHeaders = "Origin, X-Requested-With, Content-Type, Accept";
            const string LSetAllowMethods = "GET, POST";
            const string LSetCredentials = "true";
            const string LSetMaxAge = "86400";

            if (LGetAllowOrigin.Count == 0)
                AHttpContext.Response.Headers.Add(AccessControlAllowOrigin, LRequestOrigin);

            if (LGetAllowHeaders.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(AccessControlAllowHeaders, LSetAllowHeaders);

            if (LGetAllowMethods.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(AccessControlAllowMethods, LSetAllowMethods);

            if (LGetAllowCredentials.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(AccessControlAllowCredentials, LSetCredentials);

            if (LGetMaxAge.Count == 0 && LRequestOrigin.Count != 0)
                AHttpContext.Response.Headers.Add(AccessControlMaxAge, LSetMaxAge);
        }
    }
}
