namespace TokanPages.UnitTests
{
    using Moq;
    using Backend.Database;
    using Backend.Shared.Models;
    using Backend.Shared.Services;
    using Backend.Core.Utilities.DateTimeService;
    using Backend.Core.Utilities.DataUtilityService;
    using Backend.Identity.Services.JwtUtilityService;
    using Microsoft.Extensions.DependencyInjection;

    public class TestBase
    {
        protected IDataUtilityService DataUtilityService { get; }
        
        protected IJwtUtilityService JwtUtilityService { get; }

        protected IDateTimeService DateTimeService { get; }
        
        private readonly DatabaseContextFactory _databaseContextFactory;
        
        protected TestBase()
        {
            DataUtilityService = new DataUtilityService();
            JwtUtilityService = new JwtUtilityService();
            DateTimeService = new DateTimeService();

            var services = new ServiceCollection();
            services.AddSingleton<DatabaseContextFactory>();
            services.AddScoped(context =>
            {
                var factory = context.GetService<DatabaseContextFactory>();
                return factory?.CreateDatabaseContext();
            });

            var serviceScope = services.BuildServiceProvider(true).CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            _databaseContextFactory = serviceProvider.GetService<DatabaseContextFactory>();
        }

        protected DatabaseContext GetTestDatabaseContext() =>  _databaseContextFactory.CreateDatabaseContext();

        protected static Mock<IApplicationSettings> MockApplicationSettings(ApplicationPaths applicationPaths = default, 
            IdentityServer identityServer = default, ExpirationSettings expirationSettings = default, 
            EmailSender emailSender = default, AzureStorage azureStorage = default, SonarQube sonarQube = default)
        {
            var applicationSettings = new Mock<IApplicationSettings>();

            var returnApplicationPaths = applicationPaths ?? new ApplicationPaths();
            applicationSettings
                .SetupGet(settings => settings.ApplicationPaths)
                .Returns(returnApplicationPaths);

            var returnIdentityServer = identityServer ?? new IdentityServer();
            applicationSettings
                .SetupGet(settings => settings.IdentityServer)
                .Returns(returnIdentityServer);

            var returnExpirationSettings = expirationSettings ?? new ExpirationSettings();
            applicationSettings
                .SetupGet(settings => settings.ExpirationSettings)
                .Returns(returnExpirationSettings);

            var returnEmailSender = emailSender ?? new EmailSender();
            applicationSettings
                .SetupGet(settings => settings.EmailSender)
                .Returns(returnEmailSender);

            var returnAzureStorage = azureStorage ?? new AzureStorage();
            applicationSettings
                .SetupGet(settings => settings.AzureStorage)
                .Returns(returnAzureStorage);

            var returnSonarQube = sonarQube ?? new SonarQube();
            applicationSettings
                .SetupGet(settings => settings.SonarQube)
                .Returns(returnSonarQube);

            return applicationSettings;
        }
    }
}