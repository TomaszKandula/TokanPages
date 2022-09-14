namespace TokanPages.WebApi.Configuration;

/// <summary>
/// Configure Redis
/// </summary>
public static class RedisSupport
{
    /// <summary>
    /// Configure Redis
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Provided configuration</param>
    public static void SetupRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = configuration.GetValue<string>("AzureRedis:ConnectionString");
            option.InstanceName = "master";
        });
    }
}