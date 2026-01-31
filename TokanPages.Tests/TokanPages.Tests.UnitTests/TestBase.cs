using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Persistence.DataAccess.Contexts;
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

    protected static OperationDbContext GetTestDatabaseContext()
    {
        var options = DatabaseContextProvider.GetTestDatabaseOptions();
        return DatabaseContextProvider.CreateDatabaseContext(options);
    }
    
    //TODO: to be removed
    protected static IConfigurationSection SetReturnValue(string value)
    {
        var mockedSection = new Mock<IConfigurationSection>();
        mockedSection
            .Setup(section => section.Value)
            .Returns(value);

        return mockedSection.Object;
    }

    protected Mock<IOptions<AppSettings>> GetMockSettings()
    {
        var mockedOptions = new Mock<IOptions<AppSettings>>();
        var apsSettings = new AppSettings
        {
            IdsIssuer = DataUtilityService.GetRandomString(),
            IdsAudience = DataUtilityService.GetRandomString(),
            IdsWebSecret = DataUtilityService.GetRandomString(),
            IdsRequireHttps = false,
            IdsWebTokenMaturity = 90,
            IdsRefreshTokenMaturity = 120,
            LimitActivationMaturity = 30,
            LimitResetMaturity = 30,
            AzStorageBaseUrl = DataUtilityService.GetRandomString(),
            AzStorageMaxFileSizeUserMedia = 4096,
            PathsTemplatesContactForm = DataUtilityService.GetRandomString(),
            PathsTemplatesNewsletter = DataUtilityService.GetRandomString(),
            EmailAddressContact = DataUtilityService.GetRandomString(),
            PathsDeploymentOrigin = DataUtilityService.GetRandomString(),
            PathsDevelopmentOrigin = DataUtilityService.GetRandomString(),
            PathsNewsletterUpdate = DataUtilityService.GetRandomString(),
            PathsNewsletterRemove = DataUtilityService.GetRandomString(),
            LimitLikesAnonymous = 25,
            LimitLikesUser = 50,
            UserNoteMaxCount = 10
        };

        mockedOptions.Setup(options => options.Value).Returns(apsSettings);
        return mockedOptions;
    }

    //TODO: to be removed
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
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeUserMedia"))
            .Returns(SetReturnValue("4096"));

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
            .Setup(configuration => configuration.GetSection("Paths_DevelopmentOrigin"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_DeploymentOrigin"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_NewsletterUpdate"))
            .Returns(mockedSection);

        mockedConfig
            .Setup(configuration => configuration.GetSection("Paths_NewsletterRemove"))
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