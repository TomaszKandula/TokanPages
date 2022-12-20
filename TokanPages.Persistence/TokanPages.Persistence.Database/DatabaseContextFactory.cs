using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Persistence.Database;

/// <summary>
/// A factory for creating derived DbContext instances when performing
/// database migrations (add, update, remove).
/// <see href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.design.idesigntimedbcontextfactory-1?view=efcore-5.0"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    /// <summary>
    /// This method requires a connection string defined in either 'appsettings.json' (linked)
    /// or user secret file that is referenced in the project file (user secret can be shared between projects).
    /// </summary>
    /// <param name="args">Input arguments.</param>
    /// <returns>Database instance.</returns>
    public DatabaseContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .AddUserSecrets<DatabaseContext>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = builder.GetConnectionString("DbConnect");
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new DatabaseContext(optionsBuilder.Options);
    }
}