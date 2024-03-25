using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TokanPages.Backend.Configuration;

/// <summary>
/// REDIS support.
/// </summary>
[ExcludeFromCodeCoverage]
public static class RedisSupport
{
    /// <summary>
    /// Setup REDIS cache.
    /// </summary>
    /// <param name="services">Service collections.</param>
    /// <param name="configuration">Application configuration instance.</param>
    public static void SetupRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = configuration.GetValue<string>("AZ_Redis_ConnectionString");
            option.InstanceName = configuration.GetValue<string>("AZ_Redis_InstanceName");
        });
    }

    public static string GetHostAndPort(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("AZ_Redis_ConnectionString");
        var data = connectionString.Split(',');
        return data.Length == 0 ? string.Empty : data[0];
    }

    public static string GetPassword(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("AZ_Redis_ConnectionString");
        var data = connectionString.Split(',');
        if (data.Length < 1)
            return string.Empty;

        var password = data[1];
        return password.Replace("password=", "");
    }

    public static bool GetSsl(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("AZ_Redis_ConnectionString");
        var data = connectionString.Split(',');
        if (data.Length < 2)
            return false;

        var ssl = data[2];
        return bool.Parse(ssl.Replace("ssl=", ""));
    }

    public static bool GetAbortConnect(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("AZ_Redis_ConnectionString");
        var data = connectionString.Split(',');
        if (data.Length < 3)
            return false;

        var ssl = data[3];
        return bool.Parse(ssl.Replace("abortConnect=", ""));
    }
}