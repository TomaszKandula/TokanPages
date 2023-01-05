using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.MigrationRunner.Abstractions;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner;

[ExcludeFromCodeCoverage]
public class DatabaseCopier : IDatabaseCopier
{
    private const string Caller = nameof(DatabaseCopier);

    /// <summary>
    /// Takes the data from the current production database and move it to the next production database.
    /// </summary>
    /// <remarks>
    /// It requires production environment to be set.
    /// </remarks>
    /// <param name="sourceConnection">Source database connection string.</param>
    /// <param name="targetConnection">Target database connection string.</param>
    /// <typeparam name="T">Type of the context for the source database and for the target database.</typeparam>
    /// <exception cref="ArgumentException">Throws an exception for unsupported type.</exception>
    public async Task RunAndCopy<T>(string sourceConnection, string targetConnection) where T : DbContext
    {
        if (Environments.IsTestingOrStaging)
            throw new Exception("Cannot perform on a non-production database context!");

        var sourceDatabase = DatabaseConnection.GetDatabaseName(sourceConnection);
        var targetDatabase = DatabaseConnection.GetDatabaseName(targetConnection);
        ConsolePrints.PrintOnInfo($"[{Caller} | {typeof(T).Name}]: Copying the production data from '{sourceDatabase}' to '{targetDatabase}'...");

        switch (typeof(T).Name)
        {
            case "DatabaseContext":
                await DatabaseContextUpdater.UpdateProduction(sourceConnection, targetConnection);
                break;
            default:
                throw new ArgumentException("Cannot copy the production data between databases. Unsupported database context!");
        }

        ConsolePrints.PrintOnSuccess($"[{Caller} | {typeof(T).Name}]: The current production database is copied to the new database!");
    }
}