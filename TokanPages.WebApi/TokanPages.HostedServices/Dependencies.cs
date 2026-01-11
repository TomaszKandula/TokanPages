using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.VideoConverterService;
using TokanPages.Services.VideoConverterService.Abstractions;
using TokanPages.Services.VideoProcessingService;
using TokanPages.Services.VideoProcessingService.Abstractions;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TokanPages.HostedServices.CronJobs;
using TokanPages.HostedServices.CronJobs.Abstractions;
using TokanPages.HostedServices.Models;
using TokanPages.HostedServices.Workers;
using TokanPages.Services.BatchService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.SpaCachingService;
using JsonSerializer = TokanPages.Backend.Core.Utilities.JsonSerializer.JsonSerializer;

namespace TokanPages.HostedServices;

/// <summary>
/// Application dependencies.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Dependencies
{
    /// <summary>
    /// Register application dependencies.
    /// </summary>
    /// <param name="services">Application service collection.</param>
    /// <param name="configuration">Application configuration.</param>
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCommonServices(configuration);
        SetupDatabase(services, configuration);
    }

    /// <summary>
    /// Register common application services.
    /// </summary>
    /// <param name="services">Application service collection.</param>
    /// <param name="configuration">Application configuration.</param>
    public static void RegisterCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        SetupLogger(services);
        SetupServices(services, configuration);
    }

    private static void SetupLogger(IServiceCollection services) 
        => services.AddSingleton<ILoggerService, LoggerService>();

    private static void SetupDatabase(IServiceCollection services, IConfiguration configuration) 
    {
        const int maxRetryCount = 10;
        var maxRetryDelay = TimeSpan.FromSeconds(5);

        services.AddDbContext<DatabaseContext>(options =>
        {
            var connectionString = configuration.GetValue<string>($"Db_{nameof(DatabaseContext)}") ?? string.Empty;
            options.UseSqlServer(connectionString, addOptions 
                => addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
        });
    }

    private static void SetupServices(IServiceCollection services, IConfiguration configuration) 
	{
		services.AddHttpContextAccessor();
		services.AddSingleton<IHttpClientServiceFactory>(_ => new HttpClientServiceFactory());

        services.AddSingleton<IJsonSerializer, JsonSerializer>();
		services.AddSingleton<IDateTimeService, DateTimeService>();
		services.AddSingleton<IVideoConverter, VideoConverter>();
		services.AddSingleton<IVideoProcessor, VideoProcessor>();
        services.AddSingleton<IBatchService, BatchService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();
        services.AddSingleton<ICachingService, CachingService>();

        services.AddSingleton<CacheProcessing>();
        services.AddSingleton<VideoProcessing>();
        services.AddSingleton<EmailProcessing>();
        services.AddHostedService<CacheProcessingWorker>();
        services.AddHostedService<VideoProcessingWorker>();
        services.AddHostedService<EmailProcessingWorker>();

        services.SetupCronServices(configuration);
        services.SetupAzureServices(configuration);
    }

    private static void SetupAzureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAzureBusFactory>(_ =>
        {
            var connectionString = configuration.GetValue<string>("AZ_Bus_ConnectionString") ?? string.Empty;
            return new AzureBusFactory(connectionString);
        });

        services.AddSingleton<IAzureBlobStorageFactory>(_ =>
        {
            var containerName = configuration.GetValue<string>("AZ_Storage_ContainerName") ?? string.Empty;
            var connectionString = configuration.GetValue<string>("AZ_Storage_ConnectionString") ?? string.Empty;
            return new AzureBlobStorageFactory(connectionString, containerName);
        });
    }

    private static void SetupCronServices(this IServiceCollection services, IConfiguration configuration)
    {
        var batchInvoicingCron = configuration.GetValue<string>("BatchInvoicing_Cron");
        var cachingServiceCron = configuration.GetValue<string>("CachingService_Cron");
        var cachingServiceGetUrl = configuration.GetValue<string>("CachingService_GetUrl");
        var cachingServicePostUrl = configuration.GetValue<string>("CachingService_PostUrl");
        var cachingServiceFiles = configuration.GetValue<string>("CachingService_Files");
        var cachingServicePaths = configuration.GetValue<string>("CachingService_Paths");
        var cachingServicePdfSource = configuration.GetValue<string>("CachingService_PdfSource");

        if (!string.IsNullOrWhiteSpace(batchInvoicingCron))
        {
            var batchProcessingConfig = new BatchProcessingConfig
            {
                TimeZoneInfo = TimeZoneInfo.Local,
                CronExpression = batchInvoicingCron
            };

            services.AddSingleton<IBatchProcessingConfig>(batchProcessingConfig);
            services.AddHostedService<BatchProcessingJob>();
        }

        if (!string.IsNullOrWhiteSpace(cachingServiceCron))
        {
            var hasFiles = !string.IsNullOrWhiteSpace(cachingServiceFiles);
            var cachingProcessingConfig = new CachingProcessingConfig
            {
                TimeZoneInfo = TimeZoneInfo.Local,
                CronExpression = cachingServiceCron,
                GetActionUrl = cachingServiceGetUrl ?? "",
                PostActionUrl = cachingServicePostUrl ?? "",
                FilesToCache = hasFiles ? cachingServiceFiles?.Split(";") : null,
                PageRoutePaths = GetSerializedList<RoutePath>(cachingServicePaths),
                PdfRoutePaths = GetSerializedList<RoutePath>(cachingServicePdfSource),
            };

            services.AddSingleton<ICachingProcessingConfig>(cachingProcessingConfig);
            services.AddHostedService<CachingProcessingJob>();
        }
    }

    /// <summary>
    /// We process configuration string from Azure Key Vault.
    /// Because Azure Key Vault keeps only strings, we must deserialize given value.
    /// </summary>
    /// <param name="source">Serialized value.</param>
    /// <returns>Deserialized object.</returns>
    private static List<T> GetSerializedList<T>(string? source)
    {
        if (source is null)
            return new List<T>();

        var result = JsonConvert.DeserializeObject<List<T>>(source);
        return result ?? new List<T>();
    }
}