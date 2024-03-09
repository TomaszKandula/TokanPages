using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.MigrationRunner;
using TokanPages.Persistence.MigrationRunner.Helpers;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework(
    "TokanPages.Tests.E2ETests.Helpers.TestFrameworkBootstrap", 
    "TokanPages.Tests.E2ETests")
]

namespace TokanPages.Tests.E2ETests.Helpers;

public class TestFrameworkBootstrap : XunitTestFramework, IDisposable
{
    private const string Caller = nameof(TestFrameworkBootstrap);

    /// <summary>
    /// Prepares the test environment before all the tests are started.
    /// </summary>
    /// <param name="messageSink">Represents an endpoint for the reception of test message.</param>
    public TestFrameworkBootstrap(IMessageSink messageSink) : base(messageSink)
    {
        ConsolePrints.PrintOnInfo($"[{Caller}]: Preparing the test environment...");

        var migrator = new DatabaseMigrator();
        var seeder = new DataSeeder();
        var configuration = DatabaseConnection.GetConfiguration<DatabaseContext>();
        var connection = DatabaseConnection.GetConnectionString<DatabaseContext>(configuration);
        DatabaseConnection.ValidateConnectionString<DatabaseContext>(connection);

        migrator.RunAndMigrate<DatabaseContext>(connection);
        seeder.Seed<DatabaseContext>(connection);
    }

    /// <summary>
    /// Clean-up code.
    /// </summary>
    public new void Dispose()
    {
        GC.SuppressFinalize(this);
        base.Dispose();
    }
}