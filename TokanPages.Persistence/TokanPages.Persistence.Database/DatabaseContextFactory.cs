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
    /// This method requires a connection string defined in either the linked application settings file or
    /// the "user secret" file referenced in the project file. The same "user secret" file can be shared between projects.      
    /// </summary>
    /// <remarks>
    /// For local development, user secrets should be used.
    /// </remarks>
    /// <param name="args">Input arguments.</param>
    /// <returns>Database instance.</returns>
    public DatabaseContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .AddUserSecrets<DatabaseContext>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = builder.GetValue<string>("Db_Connection");
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new DatabaseContext(optionsBuilder.Options);
    }
}