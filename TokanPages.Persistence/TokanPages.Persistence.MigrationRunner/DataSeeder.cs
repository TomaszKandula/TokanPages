using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.MigrationRunner.Abstractions;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;

public class DataSeeder : IDataSeeder
{
    private const string Caller = nameof(DataSeeder);

    /// <summary>
    /// Seeds the test data for supported database context.
    /// </summary>
    /// <remarks>
    /// It removes database content before seeding new one.
    /// </remarks>
    /// <param name="connectionString">Connection string to a database.</param>
    /// <param name="contextName">Name of the database context.</param>
    /// <typeparam name="T">Type of the database context.</typeparam>
    /// <exception cref="Exception">Throws when connection fail.</exception>
    /// <exception cref="ArgumentException">Throws when unsupported context is passed.</exception>
    public void Seed<T>(string connectionString, string contextName) where T : DbContext
    {
        if (!Environments.IsTestingOrStaging)
        {
            ConsolePrints.PrintOnWarning($"[{Caller} | {contextName}]: Cannot seed the test data to a production... skipped.");
            return;
        }

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Creating context...");

        var options = DatabaseOptions.GetOptions<T>(connectionString, Environments.IsTestingOrStaging);
        var context = (T)Activator.CreateInstance(typeof(T), options)!;

        ConsolePrints.PrintOnSuccess($"[{Caller} | {contextName}]: Context created successfully!");
        
        if (!context.Database.CanConnect())
            throw new Exception($"Cannot connect to the database for context '{contextName}'!");

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Database update started...");

        switch (context)
        {
            case DatabaseContext databaseContext:
                DatabaseContextUpdater.RemoveTestData(databaseContext);
                DatabaseContextUpdater.PopulateTestData(databaseContext);
                break;
            default:
                throw new ArgumentException("Cannot seed the test data. Unsupported database context!");
        }
    }
}