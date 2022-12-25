using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.MigrationRunner.DataSeeder.Abstractions;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder;

public class DataSeeder : IDataSeeder
{
    public void Seed<T>(string connectionString, string contextName) where T : DbContext
    {
        throw new NotImplementedException();
    }
}