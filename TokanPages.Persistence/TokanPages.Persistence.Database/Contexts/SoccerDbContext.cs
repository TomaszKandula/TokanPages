using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Persistence.Database.Contexts;

[ExcludeFromCodeCoverage]
public class SoccerDbContext : DbContext
{
    public SoccerDbContext(DbContextOptions<SoccerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfiguration(modelBuilder);
    }

    private static void ApplyConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("soccer");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}