namespace TokanPages.WebApi.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    public class CustomCacheControl
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomCacheControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

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