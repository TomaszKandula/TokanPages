using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Seeders;

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

        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// It will create the database if it does not already exist, and
        /// SQLite in-memory database is not in use.
        /// </summary>
        public async Task StartMigration()
        {
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                await Database.OpenConnectionAsync();
                await Database.EnsureCreatedAsync();
                return;
            }

            await Database.MigrateAsync();
        }

        /// <summary>
        /// Adds default values to the database if tables are empty (newly created).
        /// </summary>
        public async Task SeedData()
        {
            if (!Users.AnyAsync().GetAwaiter().GetResult())
                Users.AddRange(UsersSeeder.SeedUsers());

            if (!Articles.AnyAsync().GetAwaiter().GetResult())
                Articles.AddRange(ArticlesSeeder.SeedArticles());

            if (!Subscribers.AnyAsync().GetAwaiter().GetResult())
                Subscribers.AddRange(SubscribersSeeder.SeedSubscribers());

            if (!ArticleLikes.AnyAsync().GetAwaiter().GetResult())
                ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());
            
            if (!PhotoCategories.AnyAsync().GetAwaiter().GetResult())
                PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());

            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder AModelBuilder)
        {
            base.OnModelCreating(AModelBuilder);
            ApplyConfiguration(AModelBuilder);
        }

        private static void ApplyConfiguration(ModelBuilder AModelBuilder) 
            => AModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
