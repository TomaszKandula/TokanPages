using System;
using System.Net.Http;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Cqrs;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Database;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Behaviours;
using TokanPages.Backend.SmtpClient.Models;
using TokanPages.Backend.Database.Initializer;
using TokanPages.Backend.Identity.Authentication;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Storage.AzureBlobStorage.Factory;
using FluentValidation;
using MailKit.Net.Smtp;
using DnsClient;
using MediatR;

namespace TokanPages.WebApi.Configuration
{
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
            JwtBearer.Configure(AServices, AConfiguration);
        }

        private static void SetupAppSettings(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddSingleton(AConfiguration.GetSection("AzureStorage").Get<AzureStorageSettingsModel>());
            AServices.AddSingleton(AConfiguration.GetSection("SmtpServer").Get<SmtpServerSettingsModel>());
            AServices.AddSingleton(AConfiguration.GetSection("AppUrls").Get<ApplicationPathsModel>());
            AServices.AddSingleton(AConfiguration.GetSection("SonarQube").Get<SonarQubeSettingsModel>());
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
            AServices.AddScoped<ITemplateHelper, TemplateHelper>();
            AServices.AddScoped<IDateTimeService, DateTimeService>();
            AServices.AddScoped<IUserProvider, UserProvider>();
            AServices.AddScoped<IDbInitializer, DbInitializer>();

            AServices.AddSingleton<IAzureBlobStorageFactory>(AProvider =>
            {
                var LAzureStorageSettings = AProvider.GetRequiredService<AzureStorageSettingsModel>();
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
            var LAppUrls = AConfiguration.GetSection("AppUrls").Get<ApplicationPathsModel>();
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
