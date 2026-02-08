using System.Diagnostics.CodeAnalysis;
using Azure.Storage.Blobs;
using HealthChecks.Azure.Storage.Blobs;
using TokanPages.Backend.Configuration;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Gateway.Extensions;
using TokanPages.Gateway.Models;
using TokanPages.Gateway.Services;
using TokanPages.Services.WebSocketService;
using TokanPages.Services.WebSocketService.Abstractions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using TokanPages.Backend.Shared.Options;

namespace TokanPages.Gateway;

/// <summary>
/// Startup.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class Startup
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
            options.Configuration.EndPoints.Add(RedisSupport.GetHostAndPort(_configuration));
            options.Configuration.Password = RedisSupport.GetPassword(_configuration);
            options.Configuration.Ssl = RedisSupport.GetSsl(_configuration);
            options.Configuration.AbortOnConnectFail = RedisSupport.GetAbortConnect(_configuration);
        });
        services.AddOptions();
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddWebSocketService();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.Configure<GatewaySettingsModel>(_configuration.GetSection(GatewaySettingsModel.SectionName));
        services.AddProxyHttpClient();
        services.AddNamedHttpClients(_configuration);

        var settings = _configuration.GetAppSettings();
        services
            .AddHealthChecks()
            .AddUrlGroup(new Uri(settings.EmailHealthUrl), name: "EmailService")
            .AddRedis(settings.AzRedisConnectionString, name: "AzureRedisCache")
            .AddSqlServer(settings.DbDatabaseContext, name: "SQLServer")
            .AddAzureBlobStorage(
                name: "AzureStorage",
                clientFactory: _ => new BlobServiceClient(settings.AzStorageConnectionString),
                optionsFactory: _ => new AzureBlobStorageHealthCheckOptions { ContainerName = settings.AzStorageContainerName })
            .AddAzureServiceBusQueue(settings.AzBusConnectionString, name: "AzureBusServiceEmail", queueName: "email_queue")
            .AddAzureServiceBusQueue(settings.AzBusConnectionString, name: "AzureBusServicePayment", queueName: "payment_queue")
            .AddAzureServiceBusQueue(settings.AzBusConnectionString, name: "AzureBusServiceVideo", queueName: "video_queue");
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