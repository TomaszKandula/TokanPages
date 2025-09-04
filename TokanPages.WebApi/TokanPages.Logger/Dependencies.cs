using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Configuration;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
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
        SetupDatabase(services, configuration);
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
        SetupLogger(services);
        SetupServices(services);
        SetupValidators(services);
        SetupMediatR(services);
        WebTokenSupport.SetupWebToken(services, configuration);
    }

    private static void SetupLogger(IServiceCollection services) 
        => services.AddSingleton<ILoggerService, LoggerService>();

    private static void SetupDatabase(IServiceCollection services, IConfiguration configuration) 
    {
        const int maxRetryCount = 10;
        var maxRetryDelay = TimeSpan.FromSeconds(5);

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetValue<string>($"Db_{nameof(DatabaseContext)}") ?? "", addOptions 
                => addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
        });
    }

    private static void SetupServices(IServiceCollection services) 
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IWebTokenUtility, WebTokenUtility>();
        services.AddScoped<IWebTokenValidation, WebTokenValidation>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IJsonSerializer, JsonSerializer>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IDataUtilityService, DataUtilityService>();
    }

    private static void SetupValidators(IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<Backend.Application.RequestHandler<IRequest, Unit>>();

    private static void SetupMediatR(IServiceCollection services) 
    {
        services.AddMediatR(options => options.AsScoped(), 
            typeof(Backend.Application.RequestHandler<IRequest, Unit>).GetTypeInfo().Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(HttpRequestBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TokenCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
    }
}