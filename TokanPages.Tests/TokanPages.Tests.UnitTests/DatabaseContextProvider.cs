using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database;

namespace TokanPages.Tests.UnitTests;

internal static class DatabaseContextProvider
{
    public static DbContextOptions<DatabaseContext> GetTestDatabaseOptions()
    {
        const string connectionString = "Data Source=InMemoryDatabase;Mode=Memory";
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlite(connectionString);

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