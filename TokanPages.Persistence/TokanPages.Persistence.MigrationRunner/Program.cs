using TokanPages.Persistence.Database;

namespace TokanPages.Persistence.MigrationRunner;

internal static class Program
{
    private static void Main(string[] args)
    {
        var databaseMigrator = new DatabaseMigrator();
        var dataSeeder = new DataSeeder();

        var connection = databaseMigrator.GetConnectionString<DatabaseContext>();
        databaseMigrator.ValidateConnectionString(connection);
        databaseMigrator.RunAndMigrate<DatabaseContext>(connection, nameof(DatabaseContext));
    }
}
