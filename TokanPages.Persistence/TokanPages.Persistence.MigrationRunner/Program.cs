using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;

internal static class Program
{
    private const string Option1 = "  --migrate         Applies any pending migrations for the context to the database.";
    private const string Option2 = "                    It will create the database if it does not already exist.";
    private const string Option3 = "  --seed            Seeds the test data to the existing database.";
    private const string Option4 = "  --migrate-seed    It will execute migration and seed the test data afterwards.";

    private static void Main(string[] args)
    {
        var arguments = InputArguments.Normalize(args);
        if (arguments is null || arguments.Count > 1)
        {
            ConsolePrints.PrintOnInfo("");
            ConsolePrints.PrintOnInfo("Migration Runner");
            ConsolePrints.PrintOnInfo("----------------");
            ConsolePrints.PrintOnInfo("");
            ConsolePrints.PrintOnInfo("It accepts only one argument. Available options:");
            ConsolePrints.PrintOnInfo("");
            ConsolePrints.PrintOnInfo(Option1);
            ConsolePrints.PrintOnInfo(Option2);
            ConsolePrints.PrintOnInfo(Option3);
            ConsolePrints.PrintOnInfo(Option4);
            ConsolePrints.PrintOnInfo("");
            return;
        }

        var connection = DatabaseConnection.GetConnectionString<DatabaseContext>();
        DatabaseConnection.ValidateConnectionString(connection);

        var migrator = new DatabaseMigrator();
        var seeder = new DataSeeder();

        foreach (var option in arguments)
        {
            switch (option)
            {
                case "--migrate":
                    migrator.RunAndMigrate<DatabaseContext>(connection, nameof(DatabaseContext));
                    ConsolePrints.PrintOnInfo("All done!");
                    break;

                case "--seed":
                    seeder.Seed<DatabaseContext>(connection, nameof(DatabaseContext));
                    ConsolePrints.PrintOnInfo("All done!");
                    break;

                case "--migrate-seed":
                    migrator.RunAndMigrate<DatabaseContext>(connection, nameof(DatabaseContext));
                    seeder.Seed<DatabaseContext>(connection, nameof(DatabaseContext));
                    ConsolePrints.PrintOnInfo("All done!");
                    break;

                default:
                    throw new ArgumentException("Unsupported option.");
            }
        }
    }
}
