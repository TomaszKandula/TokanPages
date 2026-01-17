using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Tests.UnitTests;

internal static class DatabaseContextProvider
{
    public static DbContextOptions<OperationDbContext> GetTestDatabaseOptions()
    {
        const string connectionString = "Data Source=InMemoryDatabase;Mode=Memory";
        var options = new DbContextOptionsBuilder<OperationDbContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlite(connectionString);

        return options.Options;
    }

    public static OperationDbContext CreateDatabaseContext(DbContextOptions<OperationDbContext> options)
    {
        var databaseContext = new OperationDbContext(options);
        databaseContext.Database.OpenConnection();
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}