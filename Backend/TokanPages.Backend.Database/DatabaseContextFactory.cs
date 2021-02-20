using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace TokanPages.Backend.Database
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var LEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var LBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{LEnvironment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var LConnectionString = LBuilder.GetConnectionString("DbConnect");

            var LOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            LOptionsBuilder.UseSqlServer(LConnectionString);
            
            return new DatabaseContext(LOptionsBuilder.Options);
        }
    }
}
