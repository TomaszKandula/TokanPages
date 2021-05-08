using System.Linq;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database.Seeders;

namespace TokanPages.Backend.Database.Initialize
{
    public class DbInitialize : IDbInitialize
    {
        private readonly DatabaseContext FDatabaseContext;

        public DbInitialize(DatabaseContext ADatabaseContext)
            => FDatabaseContext = ADatabaseContext;

        public void StartMigration()
        {
            if (FDatabaseContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                FDatabaseContext.Database.OpenConnection();
                FDatabaseContext.Database.EnsureCreated();
                return;
            }
        
            FDatabaseContext.Database.Migrate();
        }

        public void SeedData()
        {
            if (!FDatabaseContext.Users.Any())
                FDatabaseContext.Users.AddRange(UsersSeeder.SeedUsers());

            if (!FDatabaseContext.Articles.Any())
                FDatabaseContext.Articles.AddRange(ArticlesSeeder.SeedArticles());

            if (!FDatabaseContext.Subscribers.Any())
                FDatabaseContext.Subscribers.AddRange(SubscribersSeeder.SeedSubscribers());

            if (!FDatabaseContext.ArticleLikes.Any())
                FDatabaseContext.ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());

            if (FDatabaseContext.PhotoCategories.Any()) 
                FDatabaseContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());

            FDatabaseContext.SaveChanges();
        }
    }
}