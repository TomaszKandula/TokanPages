using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities.Soccer;
using Attribute = TokanPages.Backend.Domain.Entities.Soccer.Attribute;

namespace TokanPages.Persistence.Database.Contexts;

[ExcludeFromCodeCoverage]
public class SoccerDbContext : DbContext
{
    public SoccerDbContext(DbContextOptions<SoccerDbContext> options) : base(options) { }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Feed> Feeds { get; set; }

    public virtual DbSet<FeedImage> FeedImages { get; set; }

    public virtual DbSet<Field> Fields { get; set; }
    
    public virtual DbSet<FieldImage> FieldImages { get; set; }
    
    public virtual DbSet<Lineup> Lineups { get; set; }
    
    public virtual DbSet<Match> Matches { get; set; }
    
    public virtual DbSet<Player> Players { get; set; }
    
    public virtual DbSet<PlayerAttribute> PlayerAttributes { get; set; }
    
    public virtual DbSet<Position> Positions { get; set; }
    
    public virtual DbSet<Quality> Qualities { get; set; }
    
    public virtual DbSet<Team> Teams { get; set; }
    
    public virtual DbSet<TeamInfo> TeamInfo { get; set; }

    public virtual DbSet<View> Views { get; set; }

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