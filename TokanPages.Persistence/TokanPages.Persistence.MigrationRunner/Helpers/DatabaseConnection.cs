using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Services;

namespace TokanPages.Persistence.MigrationRunner.Helpers;

public static class DatabaseConnection
{
    private const string Caller = nameof(DatabaseConnection);

    public static IConfiguration GetConfiguration<T>() where T : DbContext
    {
        var appSettingsEnv = $"appsettings.{Environments.CurrentValue}.json";
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<T>(true)
            .AddEnvironmentVariables()
            .Build();

        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Using '{appSettingsEnv}'...");
        ConsolePrints.PrintOnWarning($"[{Caller} | {typeof(T).Name}]: User secrets (if present) will overwrite '{appSettingsEnv}'...");

        return configuration;
    }

    public static string GetConnectionString<T>(IConfiguration configuration) where T : DbContext
    {
        var databaseName = $"Db_{typeof(T).Name}_Migrator";
        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Getting connection for '{databaseName}'...");
        return configuration.GetValue<string>($"{databaseName}");
    }

    public static string GetNextProductionDatabase<T>(string sourceConnection) where T : DbContext
    {
        if (Environments.IsTestingOrStaging)
            throw new Exception("Cannot perform on a non-production database context!");

        var targetConnection = new SqlConnectionStringBuilder(sourceConnection);
        var currentName = targetConnection.InitialCatalog;

        var elements = currentName.Split("-");
        if (elements.Length is 0 or < 4)
            throw new Exception("Invalid catalog name!");

        var next = int.Parse(elements[3]) + 1;
        var newName = $"{elements[0]}-{elements[1]}-{elements[2]}-{next}";
        targetConnection.InitialCatalog = newName;

        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Current production database name is '{currentName}'.");
        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: New production database name is '{newName}'.");
        return targetConnection.ToString();
    }

    public static void ValidateConnectionString<T>(string sourceConnection) where T : DbContext
    {
        try
        {
            var connection = new SqlConnectionStringBuilder(sourceConnection);
            ConsolePrints.PrintOnSuccess($"[{Caller} | {typeof(T).Name}]: Using '{connection.InitialCatalog}' from '{connection.DataSource}'.");
        }
        catch (Exception exception)
        {
            ConsolePrints.PrintOnError($"[{Caller} | {typeof(T).Name}]: {exception.Message}");
        }
    }

    public static string GetDatabaseName(string sourceConnection)
    {
        try
        {
            var connection = new SqlConnectionStringBuilder(sourceConnection);
            return connection.InitialCatalog;
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}