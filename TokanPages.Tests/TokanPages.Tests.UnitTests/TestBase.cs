using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Persistence.Database;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Tests.UnitTests;

public abstract class TestBase
{
    protected readonly IDataUtilityService DataUtilityService;

    protected readonly IWebTokenUtility WebTokenUtility;

    protected readonly IDateTimeService DateTimeService;

    protected TestBase()
    {
        var services = new ServiceCollection();
        services.AddScoped<IDataUtilityService, DataUtilityService>();
        services.AddScoped<IWebTokenUtility, WebTokenUtility>();
        services.AddScoped<IDateTimeService, DateTimeService>();

        using var scope = services.BuildServiceProvider(true).CreateScope();
        var serviceProvider = scope.ServiceProvider;

        DataUtilityService = serviceProvider.GetRequiredService<IDataUtilityService>();
        WebTokenUtility = serviceProvider.GetRequiredService<IWebTokenUtility>();
        DateTimeService = serviceProvider.GetRequiredService<IDateTimeService>();
    }

    protected static DatabaseContext GetTestDatabaseContext()
    {
        var options = DatabaseContextProvider.GetTestDatabaseOptions();
        return DatabaseContextProvider.CreateDatabaseContext(options);
    }
    
    protected static IConfigurationSection SetReturnValue(string value)
    {
        var mockedSection = new Mock<IConfigurationSection>();
        mockedSection
            .Setup(section => section.Value)
            .Returns(value);

        return mockedSection.Object;
    }

    protected Mock<IConfiguration> SetConfiguration()
    {
        var mockedConfig = new Mock<IConfiguration>();
        var mockedSection = SetReturnValue(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetSection("Ids_Issuer"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Ids_Audience"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Ids_WebSecret"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Ids_RequireHttps"))
            .Returns(SetReturnValue("false"));

        mockedConfig
            .Setup(configuration => configuration.GetSection("Ids_WebToken_Maturity"))
            .Returns(SetReturnValue("90"));

        mockedConfig
            .Setup(configuration => configuration.GetSection("Ids_RefreshToken_Maturity"))
            .Returns(SetReturnValue("120"));

        mockedConfig
            .Setup(configuration => configuration.GetSection("Limit_Activation_Maturity"))
            .Returns(SetReturnValue("30"));

        mockedConfig
            .Setup(configuration => configuration.GetSection("Limit_Reset_Maturity"))
            .Returns(SetReturnValue("30"));

        mockedConfig
            .Setup(configuration => configuration.GetSection("AZ_Storage_BaseUrl"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_Templates_ContactForm"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_Templates_Newsletter"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Email_Address_Contact"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_DeploymentOrigin"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_UpdateSubscriber"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_Unsubscribe"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Limit_Likes_Anonymous"))
            .Returns(SetReturnValue("25"));

        mockedConfig
            .Setup(configuration => configuration.GetSection("Limit_Likes_User"))
            .Returns(SetReturnValue("50"));

        return mockedConfig;
    }
}