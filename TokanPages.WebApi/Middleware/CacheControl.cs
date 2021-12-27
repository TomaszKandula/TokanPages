namespace TokanPages.WebApi.Middleware
{
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    [ExcludeFromCodeCoverage]
    public class CacheControl
    {
        private readonly RequestDelegate _requestDelegate;

        public CacheControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };

            await _requestDelegate(httpContext);
        }
    }
}