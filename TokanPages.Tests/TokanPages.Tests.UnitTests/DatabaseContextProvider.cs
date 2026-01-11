using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Tests.UnitTests;

internal static class DatabaseContextProvider
{
    public static DbContextOptions<OperationsDbContext> GetTestDatabaseOptions()
    {
        const string connectionString = "Data Source=InMemoryDatabase;Mode=Memory";
        var options = new DbContextOptionsBuilder<OperationsDbContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlite(connectionString);

        return options.Options;
    }

    public static OperationsDbContext CreateDatabaseContext(DbContextOptions<OperationsDbContext> options)
    {
        var databaseContext = new OperationsDbContext(options);
        databaseContext.Database.OpenConnection();
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}