using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.MigrationRunner.Helpers;

public static class DatabaseOptions
{
    public static DbContextOptions<T> GetOptions<T>(string connectionString, bool isTesting = false) where T : DbContext
    {
        var builder = new DbContextOptionsBuilder<T>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .UseSqlServer(connectionString);

        if (isTesting)
            builder.EnableSensitiveDataLogging();

        return builder.Options;
    }
}