using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.Database.Initializer.Seeders;

namespace TokanPages.Persistence.Database.Initializer;

[ExcludeFromCodeCoverage]
public class DbInitializer : IDbInitializer
{
    private readonly DatabaseContext _databaseContext;

    public DbInitializer(DatabaseContext databaseContext) => _databaseContext = databaseContext;

    public void StartMigration()
    {
        ConsolePrints.PrintOnInfo("[DbInitializer]: Applying pending migrations for the context to the database...");
        _databaseContext.Database.Migrate();
        ConsolePrints.PrintOnSuccess("[DbInitializer]: Database has been migrated!");
    }

    public void SeedData()
    {
        if (!_databaseContext.Users.Any())
        {
            _databaseContext.Users.AddRange(UsersSeeder.SeedUsers());
            PrintInfo(UsersSeeder.SeedUsers().Count(), nameof(_databaseContext.Users));
        }

        if (!_databaseContext.UserInfo.Any())
        {
            _databaseContext.UserInfo.AddRange(UserInfoSeeder.SeedUserInfo());
            PrintInfo(UserInfoSeeder.SeedUserInfo().Count(), nameof(_databaseContext.UserInfo));
        }

        if (!_databaseContext.Articles.Any())
        {
            _databaseContext.Articles.AddRange(ArticlesSeeder.SeedArticles());
            PrintInfo(ArticlesSeeder.SeedArticles().Count(), nameof(_databaseContext.Articles));
        }

        if (!_databaseContext.Subscribers.Any())
        {
            _databaseContext.Subscribers.AddRange(SubscribersSeeder.SeedSubscribers());
            PrintInfo(SubscribersSeeder.SeedSubscribers().Count(), nameof(_databaseContext.Subscribers));
        }

        if (!_databaseContext.ArticleLikes.Any())
        {
            _databaseContext.ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());
            PrintInfo(ArticleLikesSeeder.SeedArticleLikes().Count(), nameof(_databaseContext.ArticleLikes));
        }

        if (!_databaseContext.PhotoCategories.Any())
        {
            _databaseContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());
            PrintInfo(PhotoCategoriesSeeder.SeedPhotoCategories().Count(), nameof(_databaseContext.PhotoCategories));
        }

        if (!_databaseContext.Roles.Any())
        {
            _databaseContext.Roles.AddRange(RolesSeeder.SeedRoles());
            PrintInfo(RolesSeeder.SeedRoles().Count(), nameof(_databaseContext.Roles));
        }

        if (!_databaseContext.Permissions.Any())
        {
            _databaseContext.Permissions.AddRange(PermissionsSeeder.SeedPermissions());
            PrintInfo(PermissionsSeeder.SeedPermissions().Count(), nameof(_databaseContext.Permissions));
        }

        if (!_databaseContext.DefaultPermissions.Any())
        {
            _databaseContext.DefaultPermissions.AddRange(DefaultPermissionsSeeder.SeedDefaultPermissions());
            PrintInfo(DefaultPermissionsSeeder.SeedDefaultPermissions().Count(), nameof(_databaseContext.DefaultPermissions));
        }

        if (!_databaseContext.UserPermissions.Any())
        {
            _databaseContext.UserPermissions.AddRange(UserPermissionsSeeder.SeedUserPermissions());
            PrintInfo(UserPermissionsSeeder.SeedUserPermissions().Count(), nameof(_databaseContext.UserPermissions));
        }

        if (!_databaseContext.UserRoles.Any())
        {
            _databaseContext.UserRoles.AddRange(UserRolesSeeder.SeedUserRoles());
            PrintInfo(UserRolesSeeder.SeedUserRoles().Count(), nameof(_databaseContext.UserRoles));
        }
            
        _databaseContext.SaveChanges();
    }

    private static void PrintInfo(int count, string entity)
    {
        ConsolePrints.PrintOnInfo($"[DbInitializer]: Adding {count} entries to the '{entity}' table...");
    }
}