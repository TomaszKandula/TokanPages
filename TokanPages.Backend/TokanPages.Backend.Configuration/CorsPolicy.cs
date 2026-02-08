using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using TokanPages.Backend.Configuration.Options;

namespace TokanPages.Backend.Configuration;

/// <summary>
/// CORS policy configuration.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class CorsPolicy
{
    /// <summary>
    /// Apply custom configuration.
    /// </summary>
    /// <param name="builder">ApplicationBuilder instance.</param>
    /// <param name="configuration">Provided configuration.</param>
    internal static void ApplyCorsPolicy(this IApplicationBuilder builder, IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        var deploymentOrigin = settings.PathsDevelopmentOrigin;
        var developmentOrigin = settings.PathsDeploymentOrigin;
        var origins = $"{deploymentOrigin};{developmentOrigin}".Split(";");

        builder.UseCors(policyBuilder =>
        {
            policyBuilder
                .WithOrigins(origins)
                .WithHeaders(
                    "X-SignalR-User-Agent",
                    "UserTimezoneOffset",
                    "UserLanguage",
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