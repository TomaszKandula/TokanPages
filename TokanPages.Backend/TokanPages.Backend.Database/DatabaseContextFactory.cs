using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace TokanPages.Backend.Database
{
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
        /// <param name="ARgs"></param>
        /// <returns></returns>
        public DatabaseContext CreateDbContext(string[] ARgs)
        {
            var LEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var LBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{LEnvironment}.json", true, true)
                .AddUserSecrets<DatabaseContext>()
                .AddEnvironmentVariables()
                .Build();

            var LConnectionString = LBuilder.GetConnectionString("DbConnect");

            var LOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            LOptionsBuilder.UseSqlServer(LConnectionString);
            
            return new DatabaseContext(LOptionsBuilder.Options);
        }
    }
}
