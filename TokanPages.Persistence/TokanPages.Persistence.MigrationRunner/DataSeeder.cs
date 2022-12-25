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

    private static readonly string? EnvironmentValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static readonly bool IsTesting = EnvironmentValue == "Testing";

    private static readonly bool IsStaging = EnvironmentValue == "Staging";

    public void Seed<T>(string connectionString, string contextName) where T : DbContext
    {
        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Creating context...");

        var options = DatabaseOptions.GetOptions<T>(connectionString, IsTesting || IsStaging);
        var context = (T)Activator.CreateInstance(typeof(T), options)!;

        ConsolePrints.PrintOnSuccess($"[{Caller} | {contextName}]: Context created successfully!");
        
        if (!context.Database.CanConnect())
            throw new Exception($"Cannot connect to the database for context '{contextName}'!");

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Database update started...");

        switch (context)
        {
            case DatabaseContext databaseContext:
                DatabaseContextUpdater.Remove(databaseContext);
                DatabaseContextUpdater.Populate(databaseContext);
                break;
            default:
                throw new ArgumentException("Cannot seed the test data. Unsupported database type!");
        }
    }
}