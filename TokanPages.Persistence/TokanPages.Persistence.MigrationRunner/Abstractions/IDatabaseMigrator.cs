using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.MigrationRunner.Abstractions;

public interface IDatabaseMigrator
{
    void RunAndMigrate<T>(string connectionString) where T : DbContext;
}