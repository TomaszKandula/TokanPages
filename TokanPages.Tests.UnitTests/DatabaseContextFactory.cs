using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;

namespace TokanPages.Tests.UnitTests
{
    internal class DatabaseContextFactory
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> FDatabaseOptions =
            new DbContextOptionsBuilder<DatabaseContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .EnableSensitiveDataLogging()
                .UseSqlite("Data Source=InMemoryDatabase;Mode=Memory");

        public DatabaseContext CreateDatabaseContext()
        {
            var LDatabaseContext = new DatabaseContext(FDatabaseOptions.Options);
            LDatabaseContext.Database.OpenConnection();
            LDatabaseContext.Database.EnsureCreated();
            return LDatabaseContext;
        }
    }
}
