using System;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;

namespace TokanPages.Tests.UnitTests
{
    internal class DatabaseContextFactory
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> FDatabaseOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        public DatabaseContext CreateDatabaseContext()
            =>  new DatabaseContext(FDatabaseOptions.Options);
    }
}
