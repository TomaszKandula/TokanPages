using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using TokanPages.Backend.Configuration;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Gateway.Extensions;
using TokanPages.Gateway.Models;
using TokanPages.Gateway.Services;
using TokanPages.Services.WebSocketService;
using TokanPages.Services.WebSocketService.Abstractions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace TokanPages.Gateway;

/// <summary>
/// Startup.
/// </summary>
[ExcludeFromCodeCoverage]
public class Startup
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Startup implementation.
    /// </summary>
    /// <param name="configuration">Application configuration.</param>
    public Startup(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Application services.
    /// </summary>
    /// <param name="services">IServiceCollection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.Configure<KestrelServerOptions>(options =>
        {
            // File size limit is controlled by the appropriate
            // handler validator that takes maximum available
            // file size from an Azure application setting.
            // However, file size cannot be larger than 2GB.
            // Integer maximum value is 2,147,483,647.
            options.Limits.MaxRequestBodySize = int.MaxValue;
            // Default values:
            // 240 bytes / sec. and 5 second of a grace period.
            // We increase values in case of slow network.
            options.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(30));
        });
        services.AddSignalR().AddStackExchangeRedis(options =>
        {
            var certificateFile = _configuration.GetValue<string>("AZ_Redis_CertificateFile");
            var certificatePass = _configuration.GetValue<string>("AZ_Redis_CertificatePass");
            var endpoints = RedisSupport.GetHostAndPort(_configuration);
            var password = RedisSupport.GetPassword(_configuration);
            var ssl = RedisSupport.GetSsl(_configuration);
            var abort = RedisSupport.GetAbortConnect(_configuration);

            options.Configuration.EndPoints.Add(endpoints);
            options.Configuration.Password = password;
            options.Configuration.Ssl = ssl;
            options.Configuration.AbortOnConnectFail = abort;
            options.Configuration.CertificateSelection += delegate
            {
                return new X509Certificate2(certificateFile!, certificatePass);
            };
        });
        services.AddOptions();
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddScoped<INotificationService, NotificationService<WebSocketHub>>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.Configure<GatewaySettings>(_configuration);
        services.AddProxyHttpClient();
        services.AddNamedHttpClients(_configuration);

        var emailHealthUrl = _configuration.GetValue<string>("Email_HealthUrl") ?? "";
        var azureRedis = _configuration.GetValue<string>("AZ_Redis_ConnectionString") ?? "";
        var sqlServer = _configuration.GetValue<string>("Db_DatabaseContext") ?? "";
        var azureStorage = _configuration.GetValue<string>("AZ_Storage_ConnectionString") ?? "";
        var azureBusService = _configuration.GetValue<string>("AZ_Bus_ConnectionString") ?? "";
        services
            .AddHealthChecks()
            .AddUrlGroup(new Uri(emailHealthUrl), name: "EmailService")
            .AddRedis(azureRedis, name: "AzureRedisCache")
            .AddSqlServer(sqlServer, name: "SQLServer")
            .AddAzureBlobStorage(azureStorage, name: "AzureStorage")
            .AddAzureServiceBusQueue(azureBusService, name: "AzureBusServiceEmail", queueName: "email_queue")
            .AddAzureServiceBusQueue(azureBusService, name: "AzureBusServicePayment", queueName: "payment_queue")
            .AddAzureServiceBusQueue(azureBusService, name: "AzureBusServiceVideo", queueName: "video_queue");
    }

    /// <summary>
    /// Application configuration.
    /// </summary>
    /// <param name="builder">IApplicationBuilder.</param>
    public void Configure(IApplicationBuilder builder)
    {
        builder.UseRouting();
        builder.ApplyGatewayCorsPolicy(_configuration);
        builder.UseMiddleware<ProxyMiddleware>();
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<WebSocketHub>("/api/websocket");
            endpoints.MapGet("/", context 
                => context.Response.WriteAsync("Gateway API"));
        });
        builder.UseHealthChecks("/hc", HealthCheckSupport.WriteResponse());
    }
}