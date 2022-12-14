﻿using Moq;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Shared.ApplicationSettings;
using TokanPages.Backend.Shared.ApplicationSettings.Models;
using TokanPages.Persistence.Database;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Tests.UnitTests;

public class TestBase
{
    private readonly DatabaseContextFactory _databaseContextFactory;

    protected readonly IDataUtilityService DataUtilityService;

    protected readonly IWebTokenUtility WebTokenUtility;

    protected readonly IDateTimeService DateTimeService;

    protected TestBase()
    {
        var services = new ServiceCollection();
        services.AddSingleton<DatabaseContextFactory>();
        services.AddScoped<IDataUtilityService, DataUtilityService>();
        services.AddScoped<IWebTokenUtility, WebTokenUtility>();
        services.AddScoped<IDateTimeService, DateTimeService>();

        using var scope = services.BuildServiceProvider(true).CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _databaseContextFactory = serviceProvider.GetRequiredService<DatabaseContextFactory>();
        DataUtilityService = serviceProvider.GetRequiredService<IDataUtilityService>();
        WebTokenUtility = serviceProvider.GetRequiredService<IWebTokenUtility>();
        DateTimeService = serviceProvider.GetRequiredService<IDateTimeService>();
    }

    protected DatabaseContext GetTestDatabaseContext() => _databaseContextFactory.CreateDatabaseContext();

    protected static Mock<IApplicationSettings> MockApplicationSettings(
        ApplicationPaths? applicationPaths = default, 
        IdentityServer? identityServer = default, 
        LimitSettings? limitSettings = default, 
        EmailSender? emailSender = default, 
        AzureStorage? azureStorage = default, 
        SonarQube? sonarQube = default)
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

        var returnExpirationSettings = limitSettings ?? new LimitSettings();
        applicationSettings
            .SetupGet(settings => settings.LimitSettings)
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