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
    private const string Option5 = "  --next-prod       It will copy the production databases to the next production database.";

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
            ConsolePrints.PrintOnInfo(Option5);
            ConsolePrints.PrintOnInfo("");
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
                    var copier = new DatabaseCopier();
                    var target = DatabaseConnection.GetNextProductionDatabase<DatabaseContext>(source);
                    migrator.RunAndMigrate<DatabaseContext>(target);
                    copier.RunAndCopy<DatabaseContext>(source, target);
                    ConsolePrints.PrintOnInfo("All done!");
                    break;

                default:
                    throw new ArgumentException("Unsupported option.");
            }
        }
    }
}
