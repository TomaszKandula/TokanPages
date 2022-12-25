using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.MigrationRunner.Abstractions;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;

public class DatabaseMigrator : IDatabaseMigrator
{
    private const string Caller = nameof(DatabaseMigrator);

    private static readonly string? EnvironmentValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static readonly bool IsTesting = EnvironmentValue == "Testing";

    private static readonly bool IsStaging = EnvironmentValue == "Staging";

    public string GetConnectionString<T>() where T : DbContext
    {
        ConsolePrints.PrintOnInfo($"[{Caller}]: Starting database migration...");

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        var appSettingsEnv = $"appsettings.{environment}.json";
        var builder = new ConfigurationBuilder()
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<T>(true)
            .AddEnvironmentVariables()
            .Build();

        ConsolePrints.PrintOnInfo($"[{Caller}]: Using '{appSettingsEnv}'...");
        ConsolePrints.PrintOnWarning($"[{Caller}]: User secrets (if present) will overwrite '{appSettingsEnv}'...");

        return builder.GetValue<string>("Db_Connection");
    }

    public void ValidateConnectionString(string connectionString)
    {
        try
        {
            var connection = new SqlConnectionStringBuilder(connectionString);
            ConsolePrints.PrintOnSuccess($"[{Caller}]: Using '{connection.InitialCatalog}' at '{connection.DataSource}'.");
        }
        catch (Exception exception)
        {
            ConsolePrints.PrintOnError($"[{Caller}]: {exception.Message}");
        }
    }

    public void RunAndMigrate<T>(string connectionString, string contextName) where T : DbContext
    {
        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Creating context...");

        var options = DatabaseOptions.GetOptions<T>(connectionString, IsTesting || IsStaging);
        var context = (T)Activator.CreateInstance(typeof(T), options)!;

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Context created successfully!");

        if (!context.Database.CanConnect())
            throw new Exception($"Cannot connect to the database for context '{contextName}'!");

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Database update started...");

        context.Database.Migrate();

        ConsolePrints.PrintOnInfo($"[{Caller} | {contextName}]: Finished database migration!");
    }
}