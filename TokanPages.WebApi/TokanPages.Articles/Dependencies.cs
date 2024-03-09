using System.Text;
using System.Reflection;
using System.Security.Claims;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Constants;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using MediatR;
using FluentValidation;
using TokanPages.Backend.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.CipheringService;
using TokanPages.Services.BehaviourService;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Persistence.Caching;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.AzureStorageService;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.RedisCacheService;
using TokanPages.Services.RedisCacheService.Abstractions;
using TokanPages.Services.UserService;
using TokanPages.Services.WebSocketService;
using TokanPages.Services.WebSocketService.Abstractions;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Articles;

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
			SetupRetryPolicyWithPolly(services, configuration, environment);
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
		SetupWebToken(services, configuration);
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

		services.AddScoped<IUserService, UserService>();
		services.AddScoped<ICipheringService, CipheringService>();
		services.AddScoped<IEmailSenderService, EmailSenderService>();
		services.AddScoped<INotificationService, NotificationService<WebSocketHub>>();

		services.AddScoped<IJsonSerializer, JsonSerializer>();
		services.AddScoped<IDateTimeService, DateTimeService>();
		services.AddScoped<IDataUtilityService, DataUtilityService>();

		services.AddScoped<IArticlesCache, ArticlesCache>();
		services.AddScoped<IContentCache, ContentCache>();
		services.AddScoped<ISubscribersCache, SubscribersCache>();
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

	private static void SetupWebToken(IServiceCollection services, IConfiguration configuration)
	{ 
		var issuer = configuration.GetValue<string>("Ids_Issuer");
		var audience = configuration.GetValue<string>("Ids_Audience");
		var webSecret = configuration.GetValue<string>("Ids_WebSecret");
		var requireHttps = configuration.GetValue<bool>("Ids_RequireHttps");

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.Audience = audience;
			options.SecurityTokenValidators.Clear();
			options.SecurityTokenValidators.Add(new SecurityHandler());
			options.SaveToken = true;
			options.RequireHttpsMetadata = requireHttps;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(webSecret)),
				ValidateIssuer = true,
				ValidIssuer = issuer,
				ValidateAudience = true,
				ValidAudience = audience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};
			options.Events = new JwtBearerEvents
			{
				OnTokenValidated = async context =>
				{
					await ValidateTokenClaims(context);
				},
				OnForbidden = context =>
				{
					context.Fail(ErrorCodes.ACCESS_DENIED);
					return Task.FromException(ReturnAccessDenied());
				},
				OnAuthenticationFailed = context =>
				{
					context.Fail(ErrorCodes.INVALID_USER_TOKEN);
					return Task.FromException(ReturnInvalidToken());
				}
			};
		});

		services.AddAuthorization(options =>
		{
			options.AddPolicy("AuthPolicy", new AuthorizationPolicyBuilder()
				.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
				.RequireAuthenticatedUser()
				.Build());

			options.AddPolicy(nameof(Policies.AccessToTokanPages), policy => policy
				.RequireRole(nameof(Roles.GodOfAsgard), nameof(Roles.EverydayUser), nameof(Roles.ArticlePublisher), 
					nameof(Roles.PhotoPublisher), nameof(Roles.CommentPublisher)));
		});
	}

	private static Task ValidateTokenClaims(TokenValidatedContext context)
	{
		var userAlias = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Name) ?? Array.Empty<Claim>();
				        
		var role = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Role) ?? Array.Empty<Claim>();
				        
		var userId = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.NameIdentifier) ?? Array.Empty<Claim>();
				        
		var firstName = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.GivenName) ?? Array.Empty<Claim>();
				        
		var lastName = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Surname) ?? Array.Empty<Claim>();
				        
		var emailAddress = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Email) ?? Array.Empty<Claim>();

		if (userAlias.Any() && role.Any() && userId.Any() && firstName.Any() && lastName.Any() && emailAddress.Any())
			return Task.CompletedTask;

		context.Fail("Provided token is invalid.");
		return Task.FromException(ReturnInvalidClaims());
	}

	private static void SetupRetryPolicyWithPolly(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		var developmentOrigin = configuration.GetValue<string>("Paths_DevelopmentOrigin");
		var deploymentOrigin = configuration.GetValue<string>("Paths_DeploymentOrigin");

		services.AddHttpClient("RetryHttpClient", options =>
		{
			options.BaseAddress = new Uri(environment.IsDevelopment() 
				? developmentOrigin 
				: deploymentOrigin);
			options.DefaultRequestHeaders.Add("Accept", ContentTypes.Json);
			options.Timeout = TimeSpan.FromMinutes(5);
			options.DefaultRequestHeaders.ConnectionClose = true;
		}).AddPolicyHandler(HttpPolicyHandlers.SetupRetry());
	}
	
	private static AccessException ReturnAccessDenied() 
		=> new (nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
	
	private static AuthorizationException ReturnInvalidToken() 
		=> new (nameof(ErrorCodes.INVALID_USER_TOKEN), ErrorCodes.INVALID_USER_TOKEN);

	private static AuthorizationException ReturnInvalidClaims() 
		=> new (nameof(ErrorCodes.INVALID_TOKEN_CLAIMS), ErrorCodes.INVALID_TOKEN_CLAIMS);
}