using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> AOptions) : base(AOptions) { }

        public virtual DbSet<Articles> Articles { get; set; }
        
        public virtual DbSet<Subscribers> Subscribers { get; set; }
        
        public virtual DbSet<Users> Users { get; set; }
        
        public virtual DbSet<ArticleLikes> ArticleLikes { get; set; }
        
        public virtual DbSet<Albums> Albums { get; set; }
        
        public virtual DbSet<Photos> Photos { get; set; }
        
        public virtual DbSet<PhotoGears> PhotoGears { get; set; }
       
        public virtual DbSet<PhotoCategories> PhotoCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder AModelBuilder)
        {
            base.OnModelCreating(AModelBuilder);
            ApplyConfiguration(AModelBuilder);
        }

        private static void ApplyConfiguration(ModelBuilder AModelBuilder) 
            => AModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}