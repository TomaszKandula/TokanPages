namespace TokanPages.WebApi.Configuration
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Hosting;
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
    using Backend.Cqrs.Services.CipheringService;
    using Backend.Shared.Services.TemplateService;
    using Backend.Shared.Services.DateTimeService;
    using Backend.Storage.AzureBlobStorage.Factory;
    using Backend.Cqrs.Services.UserServiceProvider;
    using Backend.Shared.Services.DataUtilityService;
    using Backend.Identity.Services.JwtUtilityService;
    using FluentValidation;
    using MailKit.Net.Smtp;
    using DnsClient;
    using MediatR;
    
    [ExcludeFromCodeCoverage]
    public static class Dependencies
    {
        public static void Register(IServiceCollection AServices, IConfiguration AConfiguration, IWebHostEnvironment AEnvironment = default)
        {
            CommonServices(AServices, AConfiguration);
            SetupDatabase(AServices, AConfiguration);
            if (AEnvironment != null)
                SetupRetryPolicyWithPolly(AServices, AConfiguration, AEnvironment);
        }

        public static void CommonServices(IServiceCollection AServices, IConfiguration AConfiguration)
        {
            SetupAppSettings(AServices, AConfiguration);
            SetupLogger(AServices);
            SetupServices(AServices);
            SetupValidators(AServices);
            SetupMediatR(AServices);
            WebToken.Configure(AServices, AConfiguration);
        }

        private static void SetupAppSettings(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddSingleton(AConfiguration.GetSection("AzureStorage").Get<AzureStorage>());
            AServices.AddSingleton(AConfiguration.GetSection("SmtpServer").Get<SmtpServer>());
            AServices.AddSingleton(AConfiguration.GetSection("AppUrls").Get<ApplicationPaths>());
            AServices.AddSingleton(AConfiguration.GetSection("SonarQube").Get<SonarQube>());
        }

        private static void SetupLogger(IServiceCollection AServices) 
            => AServices.AddSingleton<ILogger, Logger>();

        private static void SetupDatabase(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            const int MAX_RETRY_COUNT = 10;
            var LMaxRetryDelay = TimeSpan.FromSeconds(5);
            
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(AConfiguration.GetConnectionString("DbConnect"), AAddOptions 
                    => AAddOptions.EnableRetryOnFailure(MAX_RETRY_COUNT, LMaxRetryDelay, null));
            });
        }

        private static void SetupServices(IServiceCollection AServices) 
        {
            AServices.AddHttpContextAccessor();

            AServices.AddScoped<HttpClient>();
            AServices.AddScoped<ISmtpClient, SmtpClient>();
            AServices.AddScoped<ILookupClient, LookupClient>();
            AServices.AddScoped<ISmtpClientService, SmtpClientService>();
            AServices.AddScoped<ITemplateService, TemplateService>();
            AServices.AddScoped<IDateTimeService, DateTimeService>();
            AServices.AddScoped<IJwtUtilityService, JwtUtilityService>();
            AServices.AddScoped<IDataUtilityService, DataUtilityService>();
            AServices.AddScoped<IUserServiceProvider, UserServiceProvider>();
            AServices.AddScoped<IDbInitializer, DbInitializer>();
            AServices.AddScoped<ICipheringService, CipheringService>();
            
            AServices.AddSingleton<IAzureBlobStorageFactory>(AProvider =>
            {
                var LAzureStorageSettings = AProvider.GetRequiredService<AzureStorage>();
                return new AzureBlobStorageFactory(LAzureStorageSettings.ConnectionString, LAzureStorageSettings.ContainerName);
            });
        }

        private static void SetupValidators(IServiceCollection AServices)
            => AServices.AddValidatorsFromAssemblyContaining<TemplateHandler<IRequest, Unit>>();

        private static void SetupMediatR(IServiceCollection AServices) 
        {
            AServices.AddMediatR(AOption => AOption.AsScoped(), 
                typeof(TemplateHandler<IRequest, Unit>).GetTypeInfo().Assembly);

            AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
        }

        private static void SetupRetryPolicyWithPolly(IServiceCollection AServices, IConfiguration AConfiguration, IWebHostEnvironment AEnvironment)
        {
            var LAppUrls = AConfiguration.GetSection("AppUrls").Get<ApplicationPaths>();
            AServices.AddHttpClient("RetryHttpClient", AOptions =>
            {
                AOptions.BaseAddress = new Uri(AEnvironment.IsDevelopment() 
                    ? LAppUrls.DevelopmentOrigin 
                    : LAppUrls.DeploymentOrigin);
                AOptions.DefaultRequestHeaders.Add("Accept", Constants.ContentTypes.JSON);
                AOptions.Timeout = TimeSpan.FromMinutes(5);
                AOptions.DefaultRequestHeaders.ConnectionClose = true;
            }).AddPolicyHandler(Handlers.RetryPolicyHandler());
        }
    }
}