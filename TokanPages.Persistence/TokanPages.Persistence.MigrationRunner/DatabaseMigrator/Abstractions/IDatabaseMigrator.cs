using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.MigrationRunner.DatabaseMigrator.Abstractions;

public interface IDatabaseMigrator
{
    string GetConnectionString<T>() where T : DbContext;

    void ValidateConnectionString(string connectionString);

    void RunAndMigrate<T>(string connectionString, string contextName) where T : DbContext;
}