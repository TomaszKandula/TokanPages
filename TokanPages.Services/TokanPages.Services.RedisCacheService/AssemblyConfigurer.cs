using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Services.RedisCacheService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddRedisCache(this IServiceCollection services)
    {
        services.AddScoped<IRedisDistributedCache, RedisDistributedCache>();
    }
}