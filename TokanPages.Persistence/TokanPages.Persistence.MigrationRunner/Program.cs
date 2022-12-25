using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database;

namespace TokanPages.Persistence.MigrationRunner;

internal static class Program
{
    private const string Caller = "MigrationRunner";

    private static readonly string? EnvironmentValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static readonly bool IsTesting = EnvironmentValue == "Testing";

    private static readonly bool IsStaging = EnvironmentValue == "Staging";

    private static void Main(string[] args)
    {
        ConsolePrints.PrintOnInfo($"[{Caller}]: Starting database migration...");

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        var appSettingsEnv = $"appsettings.{environment}.json";
        var builder = new ConfigurationBuilder()
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<DatabaseContext>(true)
            .AddEnvironmentVariables()
            .Build();

        ConsolePrints.PrintOnInfo($"[{Caller}]: Using '{appSettingsEnv}'...");
        ConsolePrints.PrintOnWarning($"[{Caller}]: User secrets (if present) will overwrite '{appSettingsEnv}'...");

        var dbConnection = builder.GetValue<string>("Db_Connection");
        try
        {
            var connection = new SqlConnectionStringBuilder(dbConnection);
            ConsolePrints.PrintOnSuccess($"[{Caller}]: Connected to {connection.DataSource}; {connection.InitialCatalog}.");
            RunAndMigrate<DatabaseContext>(dbConnection, nameof(DatabaseContext));
        }
        catch (Exception exception)
        {
            ConsolePrints.PrintOnError($"[{Caller}]: {exception.Message}");
        }
    }

    private static void RunAndMigrate<T>(string connectionString, string contextName) where T : DbContext
    {
        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Creating context...");

        var options = GetDatabaseOptions<T>(connectionString);
        var context = (T)Activator.CreateInstance(typeof(T), options)!;

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Context created successfully!");

        if (!context.Database.CanConnect())
            throw new Exception($"Cannot connect to the database for context '{contextName}'!");

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Database update started...");

        context.Database.Migrate();

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Finished database migration!");
    }

    private static DbContextOptions<T> GetDatabaseOptions<T>(string connectionString) where T : DbContext
    {
        var builder = new DbContextOptionsBuilder<T>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .UseSqlServer(connectionString);

        if (IsTesting || IsStaging)
            builder.EnableSensitiveDataLogging();

        return builder.Options;
    }
}
