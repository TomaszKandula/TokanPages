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
    /// <param name="contextName">Name of the database context.</param>
    /// <typeparam name="T">Type of the database context.</typeparam>
    /// <exception cref="Exception">Throws when connection fail.</exception>
    public void RunAndMigrate<T>(string connectionString, string contextName) where T : DbContext
    {
        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Creating context...");

        var options = DatabaseOptions.GetOptions<T>(connectionString, Environments.IsTestingOrStaging);
        var context = (T)Activator.CreateInstance(typeof(T), options)!;

        ConsolePrints.PrintOnSuccess($"[{Caller} | {contextName}]: Context created successfully!");

        if (!context.Database.CanConnect())
            throw new Exception($"Cannot connect to the database for context '{contextName}'!");

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Database update started...");

        context.Database.Migrate();

        ConsolePrints.PrintOnSuccess($"[{Caller} | {contextName}]: Finished database migration!");
    }
}