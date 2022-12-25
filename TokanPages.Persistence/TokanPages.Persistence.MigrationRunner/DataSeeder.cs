using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.MigrationRunner.Abstractions;

namespace TokanPages.Persistence.MigrationRunner;

public class DataSeeder : IDataSeeder
{
    public void Seed<T>(string connectionString, string contextName) where T : DbContext
    {
        throw new NotImplementedException();
    }
}