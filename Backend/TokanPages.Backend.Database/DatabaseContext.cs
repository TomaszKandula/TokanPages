using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> AOptions) : base(AOptions)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Subscribers> Subscribers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder AModelBuilder)
        {
            base.OnModelCreating(AModelBuilder);
            ApplyConfiguration(AModelBuilder);
        }

        private void ApplyConfiguration(ModelBuilder AModelBuilder) 
        {
            AModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
