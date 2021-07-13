namespace TokanPages.Backend.Database.Initializer
{
    using System.Linq;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Seeders;

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

            if (!FDatabaseContext.PhotoCategories.Any()) 
                FDatabaseContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());

            if (!FDatabaseContext.Roles.Any()) 
                FDatabaseContext.Roles.AddRange(RolesSeeder.SeedRoles());

            if (!FDatabaseContext.Permissions.Any()) 
                FDatabaseContext.Permissions.AddRange(PermissionsSeeder.SeedPermissions());

            if (!FDatabaseContext.DefaultPermissions.Any()) 
                FDatabaseContext.DefaultPermissions.AddRange(DefaultPermissionsSeeder.SeedDefaultPermissions());

            if (!FDatabaseContext.UserPermissions.Any()) 
                FDatabaseContext.UserPermissions.AddRange(UserPermissionsSeeder.SeedUserPermissions());

            if (!FDatabaseContext.UserRoles.Any()) 
                FDatabaseContext.UserRoles.AddRange(UserRolesSeeder.SeedUserRoles());
            
            FDatabaseContext.SaveChanges();
        }
    }
}