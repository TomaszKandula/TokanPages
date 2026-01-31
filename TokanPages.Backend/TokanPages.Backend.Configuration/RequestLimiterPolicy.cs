using System.Diagnostics.CodeAnalysis;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Configuration.Options;

namespace TokanPages.Backend.Configuration;

[ExcludeFromCodeCoverage]
public static class RequestLimiterPolicy
{
    private const string XForwardedFor = "X-Forwarded-For";

    private const string Localhost = "127.0.0.1";

    public static void AddLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSettings();
        services.AddRateLimiter(options =>
        {
            var window = settings.RequestLimiterWindow;
            var permit = settings.RequestLimiterPermit;
            var segments = settings.RequestLimiterSegments;
            var limiterOptions = new SlidingWindowRateLimiterOptions
            {
                SegmentsPerWindow = segments,
                Window = TimeSpan.FromSeconds(window),
                PermitLimit = permit
            };

            options.RejectionStatusCode = 429;
            options.AddPolicy("SigninRateLimiter", httpContext =>
            {
                var ipAddress = httpContext.GetRequestIpAddress();
                return RateLimitPartition.GetSlidingWindowLimiter(ipAddress, _ => limiterOptions);
            });
        });
    }

    private static string GetRequestIpAddress(this HttpContext httpContext) 
    {
        var remoteIpAddress = httpContext.Request.Headers[XForwardedFor].ToString();
        return string.IsNullOrEmpty(remoteIpAddress) 
            ? Localhost 
            : remoteIpAddress.Split(':')[0];
    }
}