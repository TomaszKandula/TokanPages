using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database;

namespace TokanPages.Tests.IntegrationTests.Factories;

internal class DatabaseContextFactory
{
    public DatabaseContext CreateDatabaseContext(string? connectionString)
    {
        var databaseOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlServer(connectionString ?? string.Empty);

        var databaseContext = new DatabaseContext(databaseOptions.Options);
        databaseContext.Database.OpenConnection();
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}