namespace TokanPages.WebApi.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class RedisSupport
{
    public static void SetupRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = configuration.GetValue<string>("AzureRedis:ConnectionString");
            option.InstanceName = "master";
        });
    }
}