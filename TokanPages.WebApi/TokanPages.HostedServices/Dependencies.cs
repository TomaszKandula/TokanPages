using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Services;
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
using TokanPages.Services.BatchService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.EmailSenderService.Abstractions;

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

		services.AddScoped<IJsonSerializer, JsonSerializer>();
		services.AddScoped<IDateTimeService, DateTimeService>();
		services.AddScoped<IVideoConverter, VideoConverter>();
		services.AddScoped<IVideoProcessor, VideoProcessor>();
        services.AddScoped<IBatchService, BatchService>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();

        services.AddScoped<VideoProcessing>();
        services.AddScoped<EmailProcessing>();
        services.AddHostedService<VideoProcessingWorker>();
        services.AddHostedService<EmailProcessingWorker>();

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
}