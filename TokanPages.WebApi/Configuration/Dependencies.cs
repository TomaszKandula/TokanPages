using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Services;
using TokanPages.Backend.Shared.Constants;
using TokanPages.Backend.Shared.Services.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using MediatR;
using FluentValidation;
using TokanPages.Services.UserService;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.CipheringService;
using TokanPages.Services.BehaviourService;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.EmailSenderService;
using TokanPages.Services.WebTokenService.Validation;
using TokanPages.Services.AzureStorageService.Factory;
using TokanPages.Persistence.Caching;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Initializer;
using TokanPages.Services.RedisCacheService;

namespace TokanPages.WebApi.Configuration;

/// <summary>
/// Register application dependencies
/// </summary>
[ExcludeFromCodeCoverage]
public static class Dependencies
{
	/// <summary>
	/// Register all services
	/// </summary>
	/// <param name="services">Service collections</param>
	/// <param name="configuration">Provided configuration</param>
	/// <param name="environment">Application host environment</param>
	public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment? environment = default)
	{
		services.CommonServices(configuration);
		SetupDatabase(services, configuration);
		if (environment != null)
			SetupRetryPolicyWithPolly(services, configuration, environment);
	}

	/// <summary>
	/// Register common services
	/// </summary>
	/// <param name="services">Service collections</param>
	/// <param name="configuration">Provided configuration</param>
	public static void CommonServices(this IServiceCollection services, IConfiguration configuration)
	{
		SetupAppSettings(services, configuration);
		SetupLogger(services);
		SetupServices(services);
		SetupValidators(services);
		SetupMediatR(services);
		SetupWebToken(services, configuration);
	}

	private static void SetupAppSettings(IServiceCollection services, IConfiguration configuration) 
	{
		services.AddSingleton(configuration.GetSection(nameof(AzureStorage)).Get<AzureStorage>());
		services.AddSingleton(configuration.GetSection(nameof(AzureRedis)).Get<AzureRedis>());
		services.AddSingleton(configuration.GetSection(nameof(EmailSender)).Get<EmailSender>());
		services.AddSingleton(configuration.GetSection(nameof(ApplicationPaths)).Get<ApplicationPaths>());
		services.AddSingleton(configuration.GetSection(nameof(SonarQube)).Get<SonarQube>());
		services.AddSingleton(configuration.GetSection(nameof(IdentityServer)).Get<IdentityServer>());
		services.AddSingleton(configuration.GetSection(nameof(LimitSettings)).Get<LimitSettings>());
		services.AddSingleton<IApplicationSettings, ApplicationSettings>();
	}

	private static void SetupLogger(IServiceCollection services) 
		=> services.AddSingleton<ILoggerService, LoggerService>();

	private static void SetupDatabase(IServiceCollection services, IConfiguration configuration) 
	{
		const int maxRetryCount = 10;
		var maxRetryDelay = TimeSpan.FromSeconds(5);
            
		services.AddDbContext<DatabaseContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("DbConnect"), addOptions 
				=> addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
		});
	}

	private static void SetupServices(IServiceCollection services) 
	{
		services.AddHttpContextAccessor();
		services.AddScoped<HttpClient>();

		services.AddScoped<IWebTokenUtility, WebTokenUtility>();
		services.AddScoped<IWebTokenValidation, WebTokenValidation>();

		services.AddScoped<IDbInitializer, DbInitializer>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<ICipheringService, CipheringService>();
		services.AddScoped<IHttpClientService, HttpClientService>();
		services.AddScoped<IEmailSenderService, EmailSenderService>();

		services.AddScoped<IJsonSerializer, JsonSerializer>();
		services.AddScoped<IDateTimeService, DateTimeService>();
		services.AddScoped<IDataUtilityService, DataUtilityService>();

		services.AddScoped<IArticlesCache, ArticlesCache>();
		services.AddScoped<IContentCache, ContentCache>();
		services.AddScoped<ISubscribersCache, SubscribersCache>();
		services.AddScoped<IUsersCache, UsersCache>();

		services.AddScoped<IRedisDistributedCache, RedisDistributedCache>();

		services.AddSingleton<IAzureBlobStorageFactory>(provider =>
		{
			var azureStorageSettings = provider.GetRequiredService<AzureStorage>();
			return new AzureBlobStorageFactory(azureStorageSettings.ConnectionString, azureStorageSettings.ContainerName);
		});
	}

	private static void SetupValidators(IServiceCollection services)
		=> services.AddValidatorsFromAssemblyContaining<Backend.Cqrs.RequestHandler<IRequest, Unit>>();

	private static void SetupMediatR(IServiceCollection services) 
	{
		services.AddMediatR(options => options.AsScoped(), 
			typeof(Backend.Cqrs.RequestHandler<IRequest, Unit>).GetTypeInfo().Assembly);

		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(HttpRequestBehaviour<,>));
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TokenCheckBehaviour<,>));
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
	}

	private static void SetupWebToken(IServiceCollection services, IConfiguration configuration)
	{ 
		var issuer = configuration.GetValue<string>("IdentityServer:Issuer");
		var audience = configuration.GetValue<string>("IdentityServer:Audience");
		var webSecret = configuration.GetValue<string>("IdentityServer:WebSecret");
		var requireHttps = configuration.GetValue<bool>("IdentityServer:RequireHttps");

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
				OnTokenValidated = tokenValidatedContext =>
				{
					ValidateTokenClaims(tokenValidatedContext);
					return Task.CompletedTask;
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

	private static void ValidateTokenClaims(TokenValidatedContext tokenValidatedContext)
	{
		var userAlias = tokenValidatedContext.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Name) ?? Array.Empty<Claim>();
				        
		var role = tokenValidatedContext.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Role) ?? Array.Empty<Claim>();
				        
		var userId = tokenValidatedContext.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.NameIdentifier) ?? Array.Empty<Claim>();
				        
		var firstName = tokenValidatedContext.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.GivenName) ?? Array.Empty<Claim>();
				        
		var lastName = tokenValidatedContext.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Surname) ?? Array.Empty<Claim>();
				        
		var emailAddress = tokenValidatedContext.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Email) ?? Array.Empty<Claim>();

		if (!userAlias.Any() || !role.Any() || !userId.Any()
		    || !firstName.Any() || !lastName.Any() || !emailAddress.Any())
		{
			tokenValidatedContext.Fail("Provided token is invalid.");
		}
	}

	private static void SetupRetryPolicyWithPolly(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		var applicationPaths = configuration.GetSection(nameof(ApplicationPaths)).Get<ApplicationPaths>();
		services.AddHttpClient("RetryHttpClient", options =>
		{
			options.BaseAddress = new Uri(environment.IsDevelopment() 
				? applicationPaths.DevelopmentOrigin 
				: applicationPaths.DeploymentOrigin);
			options.DefaultRequestHeaders.Add("Accept", ContentTypes.Json);
			options.Timeout = TimeSpan.FromMinutes(5);
			options.DefaultRequestHeaders.ConnectionClose = true;
		}).AddPolicyHandler(HttpPolicyHandlers.SetupRetry());
	}
}