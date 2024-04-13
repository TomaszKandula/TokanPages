using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using MediatR;
using FluentValidation;
using TokanPages.Backend.Configuration;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.CipheringService;
using TokanPages.Services.BehaviourService;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Persistence.Caching;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.RedisCacheService;
using TokanPages.Services.RedisCacheService.Abstractions;
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
		SetupServices(services, configuration);
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
			options.UseSqlServer(configuration.GetValue<string>($"Db_{nameof(DatabaseContext)}"), addOptions 
				=> addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
		});
	}

	private static void SetupServices(IServiceCollection services, IConfiguration configuration) 
	{
		services.AddHttpContextAccessor();
		services.AddSingleton<IHttpClientServiceFactory>(_ => new HttpClientServiceFactory());
		services.AddScoped<IWebTokenUtility, WebTokenUtility>();
		services.AddScoped<IWebTokenValidation, WebTokenValidation>();
		services.AddScoped<ICipheringService, CipheringService>();
		services.AddScoped<IUserService, UserService>();

		services.AddScoped<IJsonSerializer, JsonSerializer>();
		services.AddScoped<IDateTimeService, DateTimeService>();
		services.AddScoped<IDataUtilityService, DataUtilityService>();

		services.AddScoped<IUsersCache, UsersCache>();
		services.AddScoped<IRedisDistributedCache, RedisDistributedCache>();

		services.AddSingleton<IAzureBusFactory>(_ =>
		{
			var connectionString = configuration.GetValue<string>("AZ_Bus_ConnectionString");
			return new AzureBusFactory(connectionString);
		});

		services.AddSingleton<IAzureBlobStorageFactory>(_ =>
		{
			var containerName = configuration.GetValue<string>("AZ_Storage_ContainerName");
			var connectionString = configuration.GetValue<string>("AZ_Storage_ConnectionString");
			return new AzureBlobStorageFactory(connectionString, containerName);
		});
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