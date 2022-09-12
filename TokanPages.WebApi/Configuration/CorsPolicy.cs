using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace TokanPages.WebApi.Configuration;

/// <summary>
/// CORS policy configuration
/// </summary>
[ExcludeFromCodeCoverage]
public static class CorsPolicy
{
    /// <summary>
    /// Apply custom configuration
    /// </summary>
    /// <param name="builder">ApplicationBuilder instance</param>
    /// <param name="configuration">Provided configuration</param>
    public static void ApplyCorsPolicy(this IApplicationBuilder builder, IConfiguration configuration)
    {
        var deploymentOrigin = configuration.GetValue<string>("ApplicationPaths:DeploymentOrigin");
        var developmentOrigin = configuration.GetValue<string>("ApplicationPaths:DevelopmentOrigin");
        var origins = $"{deploymentOrigin};{developmentOrigin}".Split(";");

        builder.UseCors(policyBuilder =>
        {
            policyBuilder
                .WithOrigins(origins)
                .WithHeaders(
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
                .WithMethods("GET", "POST")
                .AllowCredentials()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
        });            
    }
}