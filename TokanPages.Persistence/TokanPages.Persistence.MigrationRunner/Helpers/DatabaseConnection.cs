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
        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Starting database migration...");

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        var appSettingsEnv = $"appsettings.{environment}.json";
        var builder = new ConfigurationBuilder()
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<T>(true)
            .AddEnvironmentVariables()
            .Build();

        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Using '{appSettingsEnv}'...");
        ConsolePrints.PrintOnWarning($"[{Caller} | {typeof(T).Name}]: User secrets (if present) will overwrite '{appSettingsEnv}'...");

        return builder.GetValue<string>($"Db_{typeof(T).Name}");
    }

    public static void ValidateConnectionString<T>(string connectionString) where T : DbContext
    {
        try
        {
            var connection = new SqlConnectionStringBuilder(connectionString);
            ConsolePrints.PrintOnSuccess($"[{Caller} | {typeof(T).Name}]: Using '{connection.InitialCatalog}' from '{connection.DataSource}'.");
        }
        catch (Exception exception)
        {
            ConsolePrints.PrintOnError($"[{Caller} | {typeof(T).Name}]: {exception.Message}");
        }
    }

    public static string GetDatabaseName(string connectionString)
    {
        try
        {
            var connection = new SqlConnectionStringBuilder(connectionString);
            return connection.InitialCatalog;
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}