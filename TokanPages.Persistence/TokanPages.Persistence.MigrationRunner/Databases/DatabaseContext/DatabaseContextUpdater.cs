using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.MigrationRunner.Seeders;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext;

public static class DatabaseContextUpdater
{
    private const string Caller = nameof(DatabaseContextUpdater);

    public static void Populate(Database.DatabaseContext databaseContext)
    {
        if (!databaseContext.Users.Any())
        {
            databaseContext.Users.AddRange(UsersSeeder.SeedUsers());
            PrintInfo(UsersSeeder.SeedUsers().Count(), nameof(databaseContext.Users));
        }

        if (!databaseContext.UserInfo.Any())
        {
            databaseContext.UserInfo.AddRange(UserInfoSeeder.SeedUserInfo());
            PrintInfo(UserInfoSeeder.SeedUserInfo().Count(), nameof(databaseContext.UserInfo));
        }

        if (!databaseContext.Articles.Any())
        {
            databaseContext.Articles.AddRange(ArticlesSeeder.SeedArticles());
            PrintInfo(ArticlesSeeder.SeedArticles().Count(), nameof(databaseContext.Articles));
        }

        if (!databaseContext.Subscribers.Any())
        {
            databaseContext.Subscribers.AddRange(SubscribersSeeder.SeedSubscribers());
            PrintInfo(SubscribersSeeder.SeedSubscribers().Count(), nameof(databaseContext.Subscribers));
        }

        if (!databaseContext.ArticleLikes.Any())
        {
            databaseContext.ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());
            PrintInfo(ArticleLikesSeeder.SeedArticleLikes().Count(), nameof(databaseContext.ArticleLikes));
        }

        if (!databaseContext.PhotoCategories.Any())
        {
            databaseContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());
            PrintInfo(PhotoCategoriesSeeder.SeedPhotoCategories().Count(), nameof(databaseContext.PhotoCategories));
        }

        if (!databaseContext.Roles.Any())
        {
            databaseContext.Roles.AddRange(RolesSeeder.SeedRoles());
            PrintInfo(RolesSeeder.SeedRoles().Count(), nameof(databaseContext.Roles));
        }

        if (!databaseContext.Permissions.Any())
        {
            databaseContext.Permissions.AddRange(PermissionsSeeder.SeedPermissions());
            PrintInfo(PermissionsSeeder.SeedPermissions().Count(), nameof(databaseContext.Permissions));
        }

        if (!databaseContext.DefaultPermissions.Any())
        {
            databaseContext.DefaultPermissions.AddRange(DefaultPermissionsSeeder.SeedDefaultPermissions());
            PrintInfo(DefaultPermissionsSeeder.SeedDefaultPermissions().Count(), nameof(databaseContext.DefaultPermissions));
        }

        if (!databaseContext.UserPermissions.Any())
        {
            databaseContext.UserPermissions.AddRange(UserPermissionsSeeder.SeedUserPermissions());
            PrintInfo(UserPermissionsSeeder.SeedUserPermissions().Count(), nameof(databaseContext.UserPermissions));
        }

        if (!databaseContext.UserRoles.Any())
        {
            databaseContext.UserRoles.AddRange(UserRolesSeeder.SeedUserRoles());
            PrintInfo(UserRolesSeeder.SeedUserRoles().Count(), nameof(databaseContext.UserRoles));
        }
            
        databaseContext.SaveChanges();
    }

    private static void PrintInfo(int count, string entity)
    {
        ConsolePrints.PrintOnInfo($"[{Caller}]: Adding {count} entries to the '{entity}' table...");
    }    
}