using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database;

namespace TokanPages.Tests.UnitTests;

internal class DatabaseContextFactory
{
    private readonly DbContextOptionsBuilder<DatabaseContext> _databaseOptions =
        new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlite("Data Source=InMemoryDatabase;Mode=Memory");

    public DatabaseContext CreateDatabaseContext()
    {
        var databaseContext = new DatabaseContext(_databaseOptions.Options);
        databaseContext.Database.OpenConnection();
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}