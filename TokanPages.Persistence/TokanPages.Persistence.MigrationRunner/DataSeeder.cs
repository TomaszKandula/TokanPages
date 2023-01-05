using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.MigrationRunner.Abstractions;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;

[ExcludeFromCodeCoverage]
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
    /// <typeparam name="T">Type of the database context.</typeparam>
    /// <exception cref="Exception">Throws an exception when connection fail.</exception>
    /// <exception cref="ArgumentException">Throws an exception when unsupported context is passed.</exception>
    public void Seed<T>(string connectionString) where T : DbContext
    {
        if (!Environments.IsTestingOrStaging)
        {
            ConsolePrints.PrintOnWarning($"[{Caller} | {typeof(T).Name}]: Cannot seed the test data to a production... skipped.");
            return;
        }

        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Creating context...");

        var options = DatabaseOptions.GetOptions<T>(connectionString, Environments.IsTestingOrStaging);
        var context = (T)Activator.CreateInstance(typeof(T), options)!;

        ConsolePrints.PrintOnSuccess($"[{Caller} | {typeof(T).Name}]: Context created successfully!");
        
        if (!context.Database.CanConnect())
            throw new Exception($"Cannot connect to the database for context '{typeof(T).Name}'!");

        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Database update started...");

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