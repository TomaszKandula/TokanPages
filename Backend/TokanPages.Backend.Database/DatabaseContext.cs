using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Seeders;

namespace TokanPages.Backend.Database
{

    public partial class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Subscribers> Subscribers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder AModelBuilder)
        {

            base.OnModelCreating(AModelBuilder);
            ApplyConfiguration(AModelBuilder);

            new UsersSeeder().Seed(AModelBuilder);
            new ArticlesSeeder().Seed(AModelBuilder);
            new SubscribersSeeder().Seed(AModelBuilder);

        }

        protected void ApplyConfiguration(ModelBuilder AModelBuilder) 
        {
            AModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
