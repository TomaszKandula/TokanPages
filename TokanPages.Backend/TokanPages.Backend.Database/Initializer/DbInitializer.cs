using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database.Initializer.Seeders;

namespace TokanPages.Backend.Database.Initializer
{
    [ExcludeFromCodeCoverage]
    public class DbInitializer : IDbInitializer
    {
        private readonly DatabaseContext FDatabaseContext;

        public DbInitializer(DatabaseContext ADatabaseContext) => FDatabaseContext = ADatabaseContext;

        public void StartMigration() => FDatabaseContext.Database.Migrate();

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

            if (FDatabaseContext.Roles.Any()) 
                FDatabaseContext.Roles.AddRange(RolesSeeder.SeedRoles());
            
            FDatabaseContext.SaveChanges();
        }
    }
}