using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using MediatR;
using FluentValidation;
using TokanPages.Backend.Configuration;
using TokanPages.Backend.Shared.Options;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.CipheringService;
using TokanPages.Services.BehaviourService;
using TokanPages.Services.HttpClientService;
using TokanPages.Persistence.Caching;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Persistence.DataAccess;
using TokanPages.Services.AzureBusService;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.CookieAccessorService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.RedisCacheService;
using TokanPages.Services.UserService;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Users;

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
        services.Configure<AppSettingsModel>(configuration.GetSection(AppSettingsModel.SectionName));
	}

	private static void SetupServices(this IServiceCollection services, IConfiguration configuration) 
	{
        services.AddHttpContextAccessor();
		services.AddLimiter(configuration);

        services.AddSingleton<ILoggerService, LoggerService>();
		services.AddSingleton<IHttpClientServiceFactory>(_ => new HttpClientServiceFactory());

        services.AddScoped<ICookieAccessor, CookieAccessor>();
		services.AddScoped<IWebTokenUtility, WebTokenUtility>();
		services.AddScoped<IWebTokenValidation, WebTokenValidation>();
		services.AddScoped<ICipheringService, CipheringService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IEmailSenderService, EmailSenderService>();

		services.AddScoped<IJsonSerializer, JsonSerializer>();
		services.AddScoped<IDateTimeService, DateTimeService>();
		services.AddScoped<IDataUtilityService, DataUtilityService>();

		services.AddScoped<IUsersCache, UsersCache>();
        services.AddRedisCache();
        services.AddAzureBus(configuration);
        services.AddAzureStorage(configuration);
	}

	private static void SetupValidators(this IServiceCollection services)
		=> services.AddValidatorsFromAssemblyContaining<Backend.Application.RequestHandler<IRequest, Unit>>();

	private static void SetupMediatR(this IServiceCollection services) 
	{
		services.AddMediatR(options => options.AsScoped(), 
			typeof(Backend.Application.RequestHandler<IRequest, Unit>).GetTypeInfo().Assembly);

        services.AddBehaviourServices();
	}
}