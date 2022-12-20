using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Initializer;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework(
    "TokanPages.Tests.EndToEndTests.Helpers.TestFrameworkBootstrap", 
    "TokanPages.Tests.EndToEndTests")
]

namespace TokanPages.Tests.EndToEndTests.Helpers;

public class TestFrameworkBootstrap : XunitTestFramework, IDisposable
{
    private DatabaseContext? _databaseContext;

    private const string Caller = nameof(TestFrameworkBootstrap);
    
    /// <summary>
    /// Prepares the test environment before all the tests are started.
    /// </summary>
    /// <param name="messageSink">Represents an endpoint for the reception of test message.</param>
    public TestFrameworkBootstrap(IMessageSink messageSink) : base(messageSink)
    {
        ConsolePrints.PrintOnInfo($"[{Caller}]: Preparing the test environment...");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        GetTestDatabase();
        RemoveTestDatabaseContent();
        MigrateTestDatabase();
    }

    /// <summary>
    /// Runs a clean-up code when all the test are finished.
    /// </summary>
    public new void Dispose()
    {
        GC.SuppressFinalize(this);
        if (_databaseContext is not null)
        {
            ConsolePrints.PrintOnInfo($"[{Caller}]: Cleaning up the test environment...");
            RemoveTestDatabaseContent();
            ConsolePrints.PrintOnWarning($"[{Caller}]: Test data have been removed.");
            ConsolePrints.PrintOnSuccess($"[{Caller}]: Completed!");
        }

        base.Dispose();
    }

    /// <summary>
    /// Return the test database instance based on given connection string.
    /// </summary>
    /// <remarks>
    /// Connection string is taken from the current environment variable, and it is always overwritten by existing user secret.
    /// During local development, it is recommended to use user secret that points to a local test database.
    /// </remarks>
    private void GetTestDatabase()
    {
        ConsolePrints.PrintOnInfo($"[{Caller}]: Getting test database instance...");

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        var appSettingsEnv = $"appsettings.{environment}.json";
        var builder = new ConfigurationBuilder()
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<DatabaseContext>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        ConsolePrints.PrintOnInfo($"[{Caller}]: Using '{appSettingsEnv}'...");
        ConsolePrints.PrintOnWarning($"[{Caller}]: User secrets (if present) will overwrite environment settings...");

        var connectionString = builder.GetConnectionString("DbConnect");
        try
        {
            var connection = new SqlConnectionStringBuilder(connectionString);
            var server = connection.DataSource;
            var database = connection.InitialCatalog;
            var options = TestDatabaseContextProvider.GetTestDatabaseOptions(connectionString);
            _databaseContext = TestDatabaseContextProvider.CreateDatabaseContext(options);
            ConsolePrints.PrintOnSuccess($"[{Caller}]: Connected with {server} ({database}).");
        }
        catch (Exception exception)
        {
            ConsolePrints.PrintOnError($"[{Caller}]: {exception.Message}");
        }
    }

    /// <summary>
    /// Populates the test database table when no data exists; otherwise, it does nothing.
    /// </summary>
    /// <remarks>
    /// It will create database if it does not already exist. 
    /// </remarks>
    private void MigrateTestDatabase()
    {
        if (_databaseContext is null)
        {
            ConsolePrints.PrintOnError($"[{Caller}]: Cannot migrate and seed the test data. The test database instance is null!");
            return;
        }

        var dbInitializer = new DbInitializer(_databaseContext);
        dbInitializer.StartMigration();
        dbInitializer.SeedData();
    }

    /// <summary>
    /// Removes all the tests data from the database tables.
    /// </summary>
    private void RemoveTestDatabaseContent()
    {
        if (_databaseContext is null)
        {
            ConsolePrints.PrintOnError($"[{Caller}]: Cannot perform clean-up. The test database instance is null!");
            return;
        }

        _databaseContext.RemoveRange(_databaseContext.Albums);
        PrintWarning(nameof(_databaseContext.Albums));
        _databaseContext.RemoveRange(_databaseContext.HttpRequests);
        PrintWarning(nameof(_databaseContext.HttpRequests));
        _databaseContext.RemoveRange(_databaseContext.Subscribers);
        PrintWarning(nameof(_databaseContext.Subscribers));
        _databaseContext.RemoveRange(_databaseContext.DefaultPermissions);
        PrintWarning(nameof(_databaseContext.DefaultPermissions));

        _databaseContext.RemoveRange(_databaseContext.ArticleCounts);
        PrintWarning(nameof(_databaseContext.ArticleCounts));
        _databaseContext.RemoveRange(_databaseContext.ArticleLikes);
        PrintWarning(nameof(_databaseContext.ArticleLikes));
        _databaseContext.RemoveRange(_databaseContext.Articles);
        PrintWarning(nameof(_databaseContext.Articles));

        _databaseContext.RemoveRange(_databaseContext.PhotoCategories);
        PrintWarning(nameof(_databaseContext.PhotoCategories));
        _databaseContext.RemoveRange(_databaseContext.PhotoGears);
        PrintWarning(nameof(_databaseContext.PhotoGears));
        _databaseContext.RemoveRange(_databaseContext.UserPhotos);
        PrintWarning(nameof(_databaseContext.UserPhotos));

        _databaseContext.RemoveRange(_databaseContext.UserPermissions);
        PrintWarning(nameof(_databaseContext.UserPermissions));
        _databaseContext.RemoveRange(_databaseContext.UserRoles);
        PrintWarning(nameof(_databaseContext.UserRoles));
        _databaseContext.RemoveRange(_databaseContext.UserRefreshTokens);
        PrintWarning(nameof(_databaseContext.UserRefreshTokens));
        _databaseContext.RemoveRange(_databaseContext.UserTokens);
        PrintWarning(nameof(_databaseContext.UserTokens));
        _databaseContext.RemoveRange(_databaseContext.UserInfo);
        PrintWarning(nameof(_databaseContext.UserInfo));
        _databaseContext.RemoveRange(_databaseContext.Users);
        PrintWarning(nameof(_databaseContext.Users));

        _databaseContext.RemoveRange(_databaseContext.Permissions);
        PrintWarning(nameof(_databaseContext.Permissions));
        _databaseContext.RemoveRange(_databaseContext.Roles);
        PrintWarning(nameof(_databaseContext.Roles));

        _databaseContext.SaveChanges();
    }

    private static void PrintWarning(string entity)
    {
        ConsolePrints.PrintOnWarning($"[{Caller}]: '{entity}' is marked for removal...");
    }
}