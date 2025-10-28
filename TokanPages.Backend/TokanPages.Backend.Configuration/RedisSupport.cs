using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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
        var certificateFile = configuration.GetValue<string>("AZ_Redis_CertificateFile");
        var certificatePass = configuration.GetValue<string>("AZ_Redis_CertificatePass");
        var hasCertificate = !string.IsNullOrEmpty(certificateFile) && !string.IsNullOrEmpty(certificatePass);

        if (hasCertificate)
        {
            var host = GetHost(configuration);
            var port = GetPort(configuration);
            var ssl = GetSsl(configuration);
            var abortOnFail = GetAbortConnect(configuration);
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { { host, port } },
                Ssl = ssl,
                KeepAlive = 0,
                AllowAdmin = true,
                ConnectTimeout = 5000,
                ConnectRetry = 5,
                SyncTimeout = 5000,
                AbortOnConnectFail = abortOnFail
            };

            configurationOptions.CertificateSelection += delegate
            {
                return new X509Certificate2(certificateFile!, certificatePass);
            };

            services.AddDistributedRedisCache(option =>
            {
                option.ConfigurationOptions = configurationOptions;
            });
        }
        else
        {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = configuration.GetValue<string>("AZ_Redis_ConnectionString");
                option.InstanceName = configuration.GetValue<string>("AZ_Redis_InstanceName");
            });
        }
    }

    public static string GetHost(IConfiguration configuration)
    {
        var data = GetConnectionData(configuration);
        if (data is null)
            return string.Empty;

        if (data.Length == 0)
            return string.Empty;

        var hostAndPort = data[0];
        var host = hostAndPort.Split(':')[0];
        return host;
    }

    public static int GetPort(IConfiguration configuration)
    {
        var data = GetConnectionData(configuration);
        if (data is null)
            return 0;

        if (data.Length == 0)
            return 0;

        var hostAndPort = data[0];
        var port = hostAndPort.Split(':')[1];
        return int.Parse(port);
    }

    public static string GetHostAndPort(IConfiguration configuration)
    {
        var data = GetConnectionData(configuration);
        if (data is null)
            return string.Empty;

        return data.Length == 0 ? string.Empty : data[0];
    }

    public static string GetPassword(IConfiguration configuration)
    {
        var data = GetConnectionData(configuration);
        if (data is null || data.Length < 1)
            return string.Empty;

        var password = data[1];
        return password.Replace("password=", "");
    }

    public static bool GetSsl(IConfiguration configuration)
    {
        var data = GetConnectionData(configuration);
        if (data is null)
            return false;

        if (data.Length < 2)
            return false;

        var ssl = data[2];
        return bool.Parse(ssl.Replace("ssl=", ""));
    }

    public static bool GetAbortConnect(IConfiguration configuration)
    {
        var data = GetConnectionData(configuration);
        if (data is null)
            return false;

        if (data.Length < 3)
            return false;

        var ssl = data[3];
        return bool.Parse(ssl.Replace("abortConnect=", ""));
    }

    private static string[]? GetConnectionData(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("AZ_Redis_ConnectionString");
        return connectionString?.Split(',');
    }
}