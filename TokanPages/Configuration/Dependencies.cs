using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Cqrs;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Behaviours;
using TokanPages.Backend.Shared.Settings;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.SmtpClient.Settings;
using TokanPages.Backend.Database.Initialize;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Core.Services.TemplateHelper;
using FluentValidation;
using MediatR;

namespace TokanPages.Configuration
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection AServices, IConfiguration AConfiguration)
        {
            CommonServices(AServices, AConfiguration);
            SetupDatabase(AServices, AConfiguration);
        }

        public static void RegisterForTests(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            CommonServices(AServices, AConfiguration);
            SetupDatabaseForTest(AServices);
        }

        private static void CommonServices(IServiceCollection AServices, IConfiguration AConfiguration)
        {
            SetupAppSettings(AServices, AConfiguration);
            SetupLogger(AServices);
            SetupServices(AServices);
            SetupValidators(AServices);
            SetupMediatR(AServices);
        }

        private static void SetupAppSettings(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddSingleton(AConfiguration.GetSection("AzureStorage").Get<AzureStorageSettings>());
            AServices.AddSingleton(AConfiguration.GetSection("SmtpServer").Get<SmtpServerSettings>());
            AServices.AddSingleton(AConfiguration.GetSection("AppUrls").Get<AppUrls>());
        }

        private static void SetupLogger(IServiceCollection AServices) 
        {
            AServices.AddSingleton<ILogger, Logger>();
        }

        private static void SetupDatabase(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(AConfiguration.GetConnectionString("DbConnect"),
                AAddOptions => AAddOptions.EnableRetryOnFailure());
            });
        }

        private static void SetupDatabaseForTest(IServiceCollection AServices)
        {
            var LDatabaseName = Guid.NewGuid().ToString();
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                AOptions.EnableSensitiveDataLogging();
                AOptions.UseInMemoryDatabase(LDatabaseName);
            });
        }

        private static void SetupServices(IServiceCollection AServices) 
        {
            AServices.AddHttpContextAccessor();
            AServices.AddScoped<ISmtpClientService, SmtpClientService>();
            AServices.AddScoped<IAzureStorageService, AzureStorageService>();
            AServices.AddScoped<ITemplateHelper, TemplateHelper>();
            AServices.AddScoped<IFileUtilityService, FileUtilityService>();
            AServices.AddScoped<IDbInitializer, DbInitializer>();
            AServices.AddScoped<IUserProvider, UserProvider>();
        }

        private static void SetupValidators(IServiceCollection AServices)
        {
            AServices.AddValidatorsFromAssemblyContaining<TemplateHandler<IRequest, Unit>>(ServiceLifetime.Scoped);
        }

        private static void SetupMediatR(IServiceCollection AServices) 
        {
            AServices.AddMediatR(AOption => AOption.AsScoped(), 
                typeof(TemplateHandler<IRequest, Unit>).GetTypeInfo().Assembly);

            AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
        }
    }
}
