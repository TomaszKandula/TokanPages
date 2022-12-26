using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.MigrationRunner.Abstractions;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;

public class DatabaseMigrator : IDatabaseMigrator
{
    private const string Caller = nameof(DatabaseMigrator);

    /// <summary>
    /// Migrates database for given database context.
    /// </summary>
    /// <param name="connectionString">Connection string to a database.</param>
    /// <typeparam name="T">Type of the database context.</typeparam>
    /// <exception cref="Exception">Throws an exception when connection fail.</exception>
    public void RunAndMigrate<T>(string connectionString) where T : DbContext
    {
        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Creating context...");

        var options = DatabaseOptions.GetOptions<T>(connectionString, Environments.IsTestingOrStaging);
        var context = (T)Activator.CreateInstance(typeof(T), options)!;

        ConsolePrints.PrintOnSuccess($"[{Caller} | {typeof(T).Name}]: Context created successfully!");

        if (!context.Database.CanConnect())
            throw new Exception($"Cannot connect to the database for context '{typeof(T).Name}'!");

        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Database update started...");

        context.Database.Migrate();

        ConsolePrints.PrintOnSuccess($"[{Caller} | {typeof(T).Name}]: Finished database migration!");
    }
}