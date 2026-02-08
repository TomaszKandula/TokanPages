using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Shared.Options;

namespace TokanPages.Backend.Configuration;

/// <summary>
/// REDIS support.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class RedisSupport
{
    /// <summary>
    /// Setup REDIS cache.
    /// </summary>
    /// <param name="services">Service collections.</param>
    /// <param name="configuration">Application configuration instance.</param>
    internal static void SetupRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = settings.AzRedisConnectionString;
            option.InstanceName = settings.AzRedisInstanceName;
        });
    }

    internal static string GetHostAndPort(IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        var connectionString = settings.AzRedisConnectionString;
        var data = connectionString.Split(',');

        return data.Length == 0 ? string.Empty : data[0];
    }

    internal static string GetPassword(IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        var connectionString = settings.AzRedisConnectionString;
        var data = connectionString.Split(',');
        if (data.Length < 1)
            return string.Empty;

        var password = data[1];
        return password.Replace("password=", "");
    }

    internal static bool GetSsl(IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        var connectionString = settings.AzRedisConnectionString;
        var data = connectionString.Split(',');
        if (data.Length < 2)
            return false;

        var ssl = data[2];
        return bool.Parse(ssl.Replace("ssl=", ""));
    }

    internal static bool GetAbortConnect(IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        var connectionString = settings.AzRedisConnectionString;
        var data = connectionString.Split(',');
        if (data.Length < 3)
            return false;

        var ssl = data[3];
        return bool.Parse(ssl.Replace("abortConnect=", ""));
    }
}