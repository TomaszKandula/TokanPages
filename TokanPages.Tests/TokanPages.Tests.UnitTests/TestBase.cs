using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility;
using TokanPages.Backend.Utility.Abstractions;
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

    protected Mock<IOptions<AppSettingsModel>> GetMockSettings()
    {
        var mockedOptions = new Mock<IOptions<AppSettingsModel>>();
        var apsSettings = new AppSettingsModel
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
            AzStorageMaxFileSizeSingleAsset = 4069,
            PathsTemplatesContactForm = DataUtilityService.GetRandomString(),
            PathsTemplatesNewsletter = DataUtilityService.GetRandomString(),
            EmailAddressContact = DataUtilityService.GetRandomString(),
            PathsDeploymentOrigin = DataUtilityService.GetRandomString(),
            PathsDevelopmentOrigin = DataUtilityService.GetRandomString(),
            PathsNewsletterUpdate = DataUtilityService.GetRandomString(),
            PathsNewsletterRemove = DataUtilityService.GetRandomString(),
            LimitLikesAnonymous = 25,
            LimitLikesUser = 50,
            UserNoteMaxCount = 10,
            UserNoteMaxSize = 1024
        };

        mockedOptions.Setup(options => options.Value).Returns(apsSettings);
        return mockedOptions;
    }
}