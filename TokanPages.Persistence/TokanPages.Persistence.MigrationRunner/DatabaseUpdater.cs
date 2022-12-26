using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.MigrationRunner.Abstractions;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;

public class DatabaseUpdater : IDatabaseUpdater
{
    private const string Caller = nameof(DatabaseUpdater);

    /// <summary>
    /// Takes the data from the current production database and move it to the next production database.
    /// </summary>
    /// <remarks>
    /// It requires production environment to be set.
    /// </remarks>
    /// <param name="sourceConnection">Source database connection string.</param>
    /// <param name="targetConnection">Target database connection string.</param>
    /// <typeparam name="T">Type of the context for the source database and for the target database.</typeparam>
    /// <exception cref="Exception">Throws an exception when the database connection fails.</exception>
    /// <exception cref="ArgumentException">Throws an exception for unsupported type.</exception>
    public void RunAndUpdate<T>(string sourceConnection, string targetConnection) where T : DbContext
    {
        if (Environments.IsTestingOrStaging)
        {
            ConsolePrints.PrintOnWarning($"[{Caller} | {typeof(T).Name}]: Cannot perform an update on a non-production... skipped.");
            return;
        }

        var sourceDatabase = DatabaseConnection.GetDatabaseName(sourceConnection);
        var targetDatabase = DatabaseConnection.GetDatabaseName(targetConnection);
        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Moving the production data from '{sourceDatabase}' to '{targetDatabase}'...");

        switch (typeof(T).Name)
        {
            case "DatabaseContext":
                DatabaseContextUpdater.UpdateProduction(sourceConnection, targetConnection);
                break;
            default:
                throw new ArgumentException("Cannot update the production data between databases. Unsupported database context!");
        }

        ConsolePrints.PrintOnSuccess($"[{Caller} | {typeof(T).Name}]: Finished production database update!");
    }
}