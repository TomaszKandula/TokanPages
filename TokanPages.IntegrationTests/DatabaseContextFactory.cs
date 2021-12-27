namespace TokanPages.IntegrationTests;

using Microsoft.EntityFrameworkCore;
using Backend.Database;

internal class DatabaseContextFactory
{
    public DatabaseContext CreateDatabaseContext(string connectionString)
    {
        var databaseOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlServer(connectionString);

        var databaseContext = new DatabaseContext(databaseOptions.Options);
        databaseContext.Database.OpenConnection();
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}