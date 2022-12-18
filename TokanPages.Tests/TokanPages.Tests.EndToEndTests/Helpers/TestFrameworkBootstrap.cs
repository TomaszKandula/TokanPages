using Microsoft.Extensions.Configuration;
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
        RemoveTestDatabaseContent();
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
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .AddUserSecrets<DatabaseContext>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = builder.GetConnectionString("DbConnectTest");
        var options = TestDatabaseContextProvider.GetTestDatabaseOptions(connectionString);
        _databaseContext = TestDatabaseContextProvider.CreateDatabaseContext(options);
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
        if (_databaseContext is null) return;

        _databaseContext.RemoveRange(_databaseContext.Albums);
        _databaseContext.RemoveRange(_databaseContext.HttpRequests);
        _databaseContext.RemoveRange(_databaseContext.Subscribers);
        _databaseContext.RemoveRange(_databaseContext.DefaultPermissions);

        _databaseContext.RemoveRange(_databaseContext.ArticleCounts);
        _databaseContext.RemoveRange(_databaseContext.ArticleLikes);
        _databaseContext.RemoveRange(_databaseContext.Articles);

        _databaseContext.RemoveRange(_databaseContext.PhotoCategories);
        _databaseContext.RemoveRange(_databaseContext.PhotoGears);
        _databaseContext.RemoveRange(_databaseContext.UserPhotos);

        _databaseContext.RemoveRange(_databaseContext.UserPermissions);
        _databaseContext.RemoveRange(_databaseContext.UserRoles);
        _databaseContext.RemoveRange(_databaseContext.UserRefreshTokens);
        _databaseContext.RemoveRange(_databaseContext.UserTokens);
        _databaseContext.RemoveRange(_databaseContext.UserInfo);
        _databaseContext.RemoveRange(_databaseContext.Users);

        _databaseContext.RemoveRange(_databaseContext.Permissions);
        _databaseContext.RemoveRange(_databaseContext.Roles);

        _databaseContext.SaveChanges();
    }
}