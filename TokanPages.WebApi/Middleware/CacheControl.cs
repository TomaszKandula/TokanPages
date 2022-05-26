namespace TokanPages.WebApi.Middleware;

using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

/// <summary>
/// Cache control middleware
/// </summary>
[ExcludeFromCodeCoverage]
public class CacheControl
{
    private readonly RequestDelegate _requestDelegate;

    /// <summary>
    /// Cache control middleware
    /// </summary>
    /// <param name="requestDelegate">RequestDelegate instance</param>
    public CacheControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

    /// <summary>
    /// Configure caching
    /// </summary>
    /// <param name="httpContext">HttpContext instance</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
        {
            NoCache = true
        };

        await _requestDelegate(httpContext);
    }
}