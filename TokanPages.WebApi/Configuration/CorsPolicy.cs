namespace TokanPages.WebApi.Configuration;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

[ExcludeFromCodeCoverage]
public static class CorsPolicy
{
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