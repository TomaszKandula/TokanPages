using Microsoft.Extensions.DependencyInjection;
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
}