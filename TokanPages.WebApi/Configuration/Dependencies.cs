namespace TokanPages.WebApi.Configuration
{
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
    using Backend.Cqrs;
    using Backend.Shared;
    using Backend.Database;
    using Backend.Shared.Models;
    using Backend.Shared.Services;
    using Backend.Core.Behaviours;
    using Backend.Database.Initializer;
	using Backend.Identity.Authorization;
    using Backend.Core.Utilities.LoggerService;
    using Backend.Core.Utilities.JsonSerializer;
    using Backend.Core.Utilities.DateTimeService;
    using Backend.Core.Utilities.TemplateService;
    using Backend.Cqrs.Services.CipheringService;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Storage.AzureBlobStorage.Factory;
    using Backend.Core.Utilities.JwtUtilityService;
    using Backend.Core.Utilities.DataUtilityService;
    using Backend.Cqrs.Services.UserServiceProvider;
    using MediatR;
    using FluentValidation;
    using Services.Caching;
    using Services.Caching.Users;
    using Services.Caching.Assets;
    using Services.Caching.Content;
    using Services.Caching.Metrics;
    using Services.Caching.Articles;
    using Services.Caching.Subscribers;

    [ExcludeFromCodeCoverage]
    public static class Dependencies
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment = default)
        {
            services.CommonServices(configuration);
            SetupDatabase(services, configuration);
            if (environment != null)
                SetupRetryPolicyWithPolly(services, configuration, environment);
        }

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
            services.AddSingleton(configuration.GetSection(nameof(ExpirationSettings)).Get<ExpirationSettings>());
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

            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IJwtUtilityService, JwtUtilityService>();
            services.AddScoped<IDataUtilityService, DataUtilityService>();
            services.AddScoped<IUserServiceProvider, UserServiceProvider>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ICipheringService, CipheringService>();
            services.AddScoped<IJsonSerializer, JsonSerializer>();
            services.AddScoped<ICustomHttpClient, CustomHttpClient>();

            services.AddScoped<IArticlesCache, ArticlesCache>();
            services.AddScoped<IAssetsCache, AssetsCache>();
            services.AddScoped<IContentCache, ContentCache>();
            services.AddScoped<IMetricsCache, MetricsCache>();
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
            => services.AddValidatorsFromAssemblyContaining<TemplateHandler<IRequest, Unit>>();

        private static void SetupMediatR(IServiceCollection services) 
        {
            services.AddMediatR(options => options.AsScoped(), 
                typeof(TemplateHandler<IRequest, Unit>).GetTypeInfo().Assembly);

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
                options.DefaultRequestHeaders.Add("Accept", Constants.ContentTypes.Json);
                options.Timeout = TimeSpan.FromMinutes(5);
                options.DefaultRequestHeaders.ConnectionClose = true;
            }).AddPolicyHandler(HttpPolicyHandlers.SetupRetry());
        }
    }
}