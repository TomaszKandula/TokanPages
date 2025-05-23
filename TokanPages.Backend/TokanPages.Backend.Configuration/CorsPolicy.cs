using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace TokanPages.Backend.Configuration;

/// <summary>
/// CORS policy configuration.
/// </summary>
[ExcludeFromCodeCoverage]
public static class CorsPolicy
{
    /// <summary>
    /// Apply custom configuration.
    /// </summary>
    /// <param name="builder">ApplicationBuilder instance.</param>
    /// <param name="configuration">Provided configuration.</param>
    public static void ApplyCorsPolicy(this IApplicationBuilder builder, IConfiguration configuration)
    {
        var deploymentOrigin = configuration.GetValue<string>("Paths_DevelopmentOrigin");
        var developmentOrigin = configuration.GetValue<string>("Paths_DeploymentOrigin");
        var origins = $"{deploymentOrigin};{developmentOrigin}".Split(";");

        builder.UseCors(policyBuilder =>
        {
            policyBuilder
                .WithOrigins(origins)
                .WithHeaders(
                    "X-SignalR-User-Agent",
                    "UserTimezoneOffset",
                    HeaderNames.Accept,
                    HeaderNames.ContentType,
                    HeaderNames.Authorization,
                    HeaderNames.XRequestedWith,
                    HeaderNames.AccessControlAllowOrigin,
                    HeaderNames.AccessControlAllowHeaders, 
                    HeaderNames.AccessControlAllowMethods,
                    HeaderNames.AccessControlAllowCredentials,
                    HeaderNames.AccessControlMaxAge)
                .WithMethods("HEAD", "OPTIONS", "TRACE", "GET", "PUT", "POST", "DELETE")
                .AllowCredentials()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
        });            
    }
}