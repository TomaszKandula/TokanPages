using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database;

namespace TokanPages.Tests.IntegrationTests.Helpers;

internal static class TestDatabaseContextProvider
{
    public static DbContextOptions<DatabaseContext> GetTestDatabaseOptions(string? connectionString)
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlServer(connectionString ?? string.Empty);

        return options.Options;
    }

    public static DatabaseContext CreateDatabaseContext(DbContextOptions<DatabaseContext> options)
    {
        var databaseContext = new DatabaseContext(options);
        databaseContext.Database.OpenConnection();
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}