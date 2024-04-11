using Microsoft.Net.Http.Headers;

namespace TokanPages.Gateway.Extensions;

/// <summary>
/// CORS Policy helper.
/// </summary>
public static class CorsPolicy
{
    private static readonly string[] Headers = {
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
        HeaderNames.AccessControlMaxAge
    };

    private static readonly string[] Methods = 
    {
        "GET", "POST", "PUT", "DELETE"
    };

    /// <summary>
    /// Apply CORS policy for gateway. 
    /// </summary>
    /// <param name="builder">Application builder instance.</param>
    /// <param name="configuration">Application configuration instance.</param>
    public static void ApplyGatewayCorsPolicy(this IApplicationBuilder builder, IConfiguration configuration)
    {
        var origins = configuration.GetValue<string>("Allowed").Split(";");
        builder.UseCors(policy => policy
            .WithOrigins(origins)
            .WithHeaders(Headers)
            .WithMethods(Methods)
            .AllowCredentials());
    }
}