using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TokanPages.Backend.Database
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var LOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            LOptionsBuilder.UseSqlServer("Server=tcp:mian-srv.database.windows.net,1433;Initial Catalog=tokanpages-db;Persist Security Info=False;User ID=AdminDb;Password=Timex#099#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new DatabaseContext(LOptionsBuilder.Options);
        }
    }
}
