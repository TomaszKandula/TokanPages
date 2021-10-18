namespace TokanPages.WebApi.Configuration
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Backend.Cqrs;
    using Backend.Shared;
    using Backend.Database;
    using Backend.SmtpClient;
    using Backend.Core.Logger;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Core.Behaviours;
    using Backend.SmtpClient.Models;
    using Backend.Database.Initializer;
    using Backend.Identity.Authentication;
    using Backend.Core.Utilities.JsonSerializer;
    using Backend.Core.Utilities.DateTimeService;
    using Backend.Core.Utilities.TemplateService;
    using Backend.Cqrs.Services.CipheringService;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Storage.AzureBlobStorage.Factory;
    using Backend.Core.Utilities.DataUtilityService;
    using Backend.Cqrs.Services.UserServiceProvider;
    using Backend.Identity.Services.JwtUtilityService;
    using FluentValidation;
    using MailKit.Net.Smtp;
    using DnsClient;
    using MediatR;

    [ExcludeFromCodeCoverage]
    public static class Dependencies
    {
        public static void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment = default)
        {
            CommonServices(services, configuration);
            SetupDatabase(services, configuration);
            if (environment != null)
                SetupRetryPolicyWithPolly(services, configuration, environment);
        }

        public static void CommonServices(IServiceCollection services, IConfiguration configuration)
        {
            SetupAppSettings(services, configuration);
            SetupLogger(services);
            SetupServices(services);
            SetupValidators(services);
            SetupMediatR(services);
            WebToken.Configure(services, configuration);
        }

        private static void SetupAppSettings(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSingleton(configuration.GetSection(nameof(AzureStorage)).Get<AzureStorage>());
            services.AddSingleton(configuration.GetSection(nameof(SmtpServer)).Get<SmtpServer>());
            services.AddSingleton(configuration.GetSection(nameof(ApplicationPaths)).Get<ApplicationPaths>());
            services.AddSingleton(configuration.GetSection(nameof(SonarQube)).Get<SonarQube>());
            services.AddSingleton(configuration.GetSection(nameof(IdentityServer)).Get<IdentityServer>());
            services.AddSingleton(configuration.GetSection(nameof(ExpirationSettings)).Get<ExpirationSettings>());
        }

        private static void SetupLogger(IServiceCollection services) 
            => services.AddSingleton<ILogger, Logger>();

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
            services.AddScoped<ISmtpClient, SmtpClient>();
            services.AddScoped<ILookupClient, LookupClient>();
            services.AddScoped<ISmtpClientService, SmtpClientService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IJwtUtilityService, JwtUtilityService>();
            services.AddScoped<IDataUtilityService, DataUtilityService>();
            services.AddScoped<IUserServiceProvider, UserServiceProvider>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ICipheringService, CipheringService>();
            services.AddScoped<IJsonSerializer, JsonSerializer>();
            services.AddScoped<ICustomHttpClient, CustomHttpClient>();

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
            }).AddPolicyHandler(Handlers.RetryPolicyHandler());
        }
    }
}