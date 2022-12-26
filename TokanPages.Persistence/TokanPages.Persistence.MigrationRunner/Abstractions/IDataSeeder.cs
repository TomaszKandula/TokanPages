using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.MigrationRunner.Abstractions;

public interface IDataSeeder
{
    void Seed<T>(string connectionString, string contextName) where T : DbContext;
}