namespace TokanPages.Backend.Database;

using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public virtual DbSet<Articles> Articles { get; set; }

    public virtual DbSet<Subscribers> Subscribers { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<ArticleLikes> ArticleLikes { get; set; }

    public virtual DbSet<ArticleCounts> ArticleCounts { get; set; }

    public virtual DbSet<Albums> Albums { get; set; }

    public virtual DbSet<Photos> Photos { get; set; }

    public virtual DbSet<PhotoGears> PhotoGears { get; set; }

    public virtual DbSet<PhotoCategories> PhotoCategories { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Permissions> Permissions { get; set; }

    public virtual DbSet<DefaultPermissions> DefaultPermissions { get; set; }

    public virtual DbSet<UserPermissions> UserPermissions { get; set; }

    public virtual DbSet<UserRoles> UserRoles { get; set; }

    public virtual DbSet<UserTokens> UserTokens { get; set; }

    public virtual DbSet<UserRefreshTokens> UserRefreshTokens { get; set; }

    public virtual DbSet<HttpRequests> HttpRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfiguration(modelBuilder);
    }

    private static void ApplyConfiguration(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}