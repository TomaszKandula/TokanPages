using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Configuration;

/// <summary>
/// Configure Redis cache.
/// </summary>
[ExcludeFromCodeCoverage]
public static class RedisSupport
{
    /// <summary>
    /// Configure Redis cache.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Provided configuration.</param>
    public static void SetupRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = configuration.GetValue<string>("AZ_Redis_ConnectionString");
            option.InstanceName = configuration.GetValue<string>("AZ_Redis_InstanceName");
        });
    }
}