using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Services;

namespace TokanPages.Persistence.MigrationRunner.Helpers;

public static class DatabaseConnection
{
    private const string Caller = nameof(DatabaseConnection);

    public static string GetConnectionString<T>() where T : DbContext
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

    public static void ValidateConnectionString(string connectionString)
    {
        try
        {
            var connection = new SqlConnectionStringBuilder(connectionString);
            ConsolePrints.PrintOnSuccess($"[{Caller}]: Using '{connection.InitialCatalog}' from '{connection.DataSource}'.");
        }
        catch (Exception exception)
        {
            ConsolePrints.PrintOnError($"[{Caller}]: {exception.Message}");
        }
    }
}