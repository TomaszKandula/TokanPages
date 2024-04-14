using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;
//TODO: upgrade migration runner
[ExcludeFromCodeCoverage]
internal static class Program
{
    private const string Line1 = "  --migrate         It applies any pending migrations for the context to the database.";
    private const string Line2 = "                    It will create the database if it does not already exist.";
    private const string Line3 = "  --seed            It seeds the test data into the existing database.";
    private const string Line4 = "  --migrate-seed    It will execute migration and seed the test data afterwards.";
    private const string Line5 = "  --next-prod       If the migration script is present, it will copy the current";
    private const string Line6 = "                    production database to the following production database.";
    private const string Line7 = "                    The following version number takes the last number and increases by one.";

    private static async Task Main(string[] args)
    {
        var arguments = InputArguments.Normalize(args);
        if (arguments.Count != 1)
        {
            WelcomeScreen();
            return;
        }

        var configuration = DatabaseConnection.GetConfiguration<DatabaseContext>();
        var source = DatabaseConnection.GetConnectionString<DatabaseContext>(configuration);
        DatabaseConnection.ValidateConnectionString<DatabaseContext>(source);

        var migrator = new DatabaseMigrator();
        var seeder = new DataSeeder();

        foreach (var option in arguments)
        {
            switch (option)
            {
                case "--migrate":
                    migrator.RunAndMigrate<DatabaseContext>(source);
                    ConsolePrints.PrintOnInfo("All done!");
                    break;

                case "--seed":
                    seeder.Seed<DatabaseContext>(source);
                    ConsolePrints.PrintOnInfo("All done!");
                    break;

                case "--migrate-seed":
                    migrator.RunAndMigrate<DatabaseContext>(source);
                    seeder.Seed<DatabaseContext>(source);
                    ConsolePrints.PrintOnInfo("All done!");
                    break;

                case "--next-prod":
                    var nextVersion = DatabaseConnection.GetNextVersion(new SqlConnectionStringBuilder(source));
                    var nextConnection = DatabaseConnection.GetNextDatabaseConnectionString<DatabaseContext>(source);
                    var migrationScript = DatabaseUpdate.BuildMigrationScriptName(nextVersion.number, nameof(DatabaseContext));
                    var hasMigrationScript = DatabaseUpdate.HasSqlScript(migrationScript);
                    if (hasMigrationScript)
                    {
                        var copier = new DatabaseCopier();
                        migrator.RunAndMigrate<DatabaseContext>(nextConnection);
                        await copier.RunAndCopy<DatabaseContext>(source, nextConnection);
                        ConsolePrints.PrintOnInfo("All done!");
                    }
                    else
                    {
                        ConsolePrints.PrintOnWarning("Migration script for the production database cannot be found!");
                        ConsolePrints.PrintOnWarning("Please verify and migrate manually if necessary.");
                        ConsolePrints.PrintOnInfo("Migration Runner has done for today.");
                    }
                    break;

                default:
                    WelcomeScreen();
                    break;
            }
        }
    }

    private static void WelcomeScreen()
    {
        ConsolePrints.PrintOnInfo("");
        ConsolePrints.PrintOnInfo("Migration Runner");
        ConsolePrints.PrintOnInfo("----------------");
        ConsolePrints.PrintOnInfo("");
        ConsolePrints.PrintOnInfo("It accepts only one argument. Available options:");
        ConsolePrints.PrintOnInfo("");
        ConsolePrints.PrintOnInfo(Line1);
        ConsolePrints.PrintOnInfo(Line2);
        ConsolePrints.PrintOnInfo(Line3);
        ConsolePrints.PrintOnInfo(Line4);
        ConsolePrints.PrintOnInfo(Line5);
        ConsolePrints.PrintOnInfo(Line6);
        ConsolePrints.PrintOnInfo(Line7);
        ConsolePrints.PrintOnInfo("");
    }
}
