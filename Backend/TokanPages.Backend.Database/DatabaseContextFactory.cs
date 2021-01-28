using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TokanPages.Backend.Database
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var LOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            LOptionsBuilder.UseSqlServer("");
            return new DatabaseContext(LOptionsBuilder.Options);
        }
    }
}
