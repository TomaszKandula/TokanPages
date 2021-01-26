using System;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;

namespace Backend.UnitTests
{
    internal class DatabaseContextFactory
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> FDatabaseOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        public DatabaseContext CreateDatabaseContext()
        {
            return new DatabaseContext(FDatabaseOptions.Options);
        }
    }
}
