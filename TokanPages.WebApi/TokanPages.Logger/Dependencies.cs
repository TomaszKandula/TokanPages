using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using MediatR;
using TokanPages.Backend.Configuration;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.BehaviourService;
using TokanPages.Services.UserService;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Logger;

/// <summary>
/// Register application dependencies.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Dependencies
{
    /// <summary>
    /// Register all services.
    /// </summary>
    /// <param name="services">Service collections.</param>
    /// <param name="configuration">Provided configuration.</param>
    /// <param name="environment">Application host environment.</param>
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment? environment = default)
    {
        services.CommonServices(configuration);
        services.AddDataLayer(configuration);
        if (environment != null)
            PollySupport.SetupRetryPolicyWithPolly(services, configuration, environment);
    }

    /// <summary>
    /// Register common services.
    /// </summary>
    /// <param name="services">Service collections.</param>
    /// <param name="configuration">Provided configuration.</param>
    public static void CommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetupServices(configuration);
        services.SetupValidators();
        services.SetupMediatR();
        services.SetupWebToken(configuration);
        services.Configure<AppSettings>(configuration.GetSection(AppSettings.SectionName));
    }

    private static void SetupServices(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddScoped<IWebTokenUtility, WebTokenUtility>();
        services.AddScoped<IWebTokenValidation, WebTokenValidation>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IJsonSerializer, JsonSerializer>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IDataUtilityService, DataUtilityService>();

        var settings = configuration.GetAppSettings();
        services.AddSingleton<IAzureBlobStorageFactory>(_ =>
        {
            var containerName = settings.AzStorageContainerName;
            var connectionString = settings.AzStorageConnectionString;
            return new AzureBlobStorageFactory(connectionString, containerName);
        });
    }

    private static void SetupValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<Backend.Application.RequestHandler<IRequest, Unit>>();

    private static void SetupMediatR(this IServiceCollection services) 
    {
        services.AddMediatR(options => options.AsScoped(), 
            typeof(Backend.Application.RequestHandler<IRequest, Unit>).GetTypeInfo().Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(HttpRequestBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TokenCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
    }
}