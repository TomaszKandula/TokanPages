namespace TokanPages.Backend.Database
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// A factory for creating derived DbContext instances when performing
    /// database migrations (add, update, remove) from terminal (using dotnet command).
    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.design.idesigntimedbcontextfactory-1?view=efcore-5.0"/>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <summary>
        /// This method requires connection string defined in either AppSettings.json (linked)
        /// or User Secret that is referenced in project file (user secret file can be shared between projects).
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DatabaseContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddUserSecrets<DatabaseContext>()
                .AddEnvironmentVariables()
                .Build();

            var connectionString = builder.GetConnectionString("DbConnect");

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);
            
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}