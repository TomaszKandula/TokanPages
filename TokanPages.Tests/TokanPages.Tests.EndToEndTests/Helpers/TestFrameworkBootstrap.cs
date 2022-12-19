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

    /// <summary>
    /// Prepares the test environment before all the tests are started.
    /// </summary>
    /// <param name="messageSink">Represents an endpoint for the reception of test message.</param>
    public TestFrameworkBootstrap(IMessageSink messageSink) : base(messageSink)
    {
        ConsolePrints.PrintOnInfo(@"[TestFrameworkBootstrap]: Preparing the test environment...");
        GetTestDatabase();
        RemoveTestDatabaseContent();
        MigrateTestDatabase();
        ConsolePrints.PrintOnSuccess(@"[TestFrameworkBootstrap]: Database has been migrated.");
    }

    /// <summary>
    /// Runs a clean-up code when all the test are finished.
    /// </summary>
    public new void Dispose()
    {
        GC.SuppressFinalize(this);
        ConsolePrints.PrintOnInfo(@"[TestFrameworkBootstrap]: Cleaning up the test environment...");
        RemoveTestDatabaseContent();
        ConsolePrints.PrintOnWarning(@"[TestFrameworkBootstrap]: Test data have been removed.");
        ConsolePrints.PrintOnSuccess(@"[TestFrameworkBootstrap]: Completed!");
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
        ConsolePrints.PrintOnInfo(@"[TestFrameworkBootstrap]: Getting test database instance...");

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        const string appSettings = "appsettings.json";
        var appSettingsEnv = $"appsettings.{environment}.json";
        var builder = new ConfigurationBuilder()
            .AddJsonFile(appSettings, true, true)
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<DatabaseContext>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        ConsolePrints.PrintOnInfo(@$"[TestFrameworkBootstrap]: Using '{appSettingsEnv}'...");
        ConsolePrints.PrintOnWarning(@"[TestFrameworkBootstrap]: User secrets (if present) will overwrite environment settings...");

        var connectionString = builder.GetConnectionString("DbConnectTest");
        var options = TestDatabaseContextProvider.GetTestDatabaseOptions(connectionString);
        _databaseContext = TestDatabaseContextProvider.CreateDatabaseContext(options);

        var connection = new SqlConnectionStringBuilder(connectionString);
        var server = connection.DataSource;
        var database = connection.InitialCatalog;
        ConsolePrints.PrintOnSuccess(@$"[TestFrameworkBootstrap]: Connected with {server} ({database}).");
    }

    /// <summary>
    /// Populates the test database table when no data exists; otherwise, it does nothing.
    /// </summary>
    /// <remarks>
    /// It will create database if it does not already exist. 
    /// </remarks>
    private void MigrateTestDatabase()
    {
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
            ConsolePrints.PrintOnError(@"[TestFrameworkBootstrap]: Test database instance is null!");
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
        ConsolePrints.PrintOnWarning(@$"[TestFrameworkBootstrap]: '{entity}' is marked for removal...");
    }
}