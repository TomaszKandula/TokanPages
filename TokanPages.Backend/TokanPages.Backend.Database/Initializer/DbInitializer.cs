namespace TokanPages.Backend.Database.Initializer;

using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Seeders;

[ExcludeFromCodeCoverage]
public class DbInitializer : IDbInitializer
{
    private readonly DatabaseContext _databaseContext;

    public DbInitializer(DatabaseContext databaseContext) => _databaseContext = databaseContext;

    public void StartMigration() => _databaseContext.Database.Migrate();

    public void SeedData()
    {
        if (!_databaseContext.Users.Any())
            _databaseContext.Users.AddRange(UsersSeeder.SeedUsers());

        if (!_databaseContext.Articles.Any())
            _databaseContext.Articles.AddRange(ArticlesSeeder.SeedArticles());

        if (!_databaseContext.Subscribers.Any())
            _databaseContext.Subscribers.AddRange(SubscribersSeeder.SeedSubscribers());

        if (!_databaseContext.ArticleLikes.Any())
            _databaseContext.ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());

        if (!_databaseContext.PhotoCategories.Any()) 
            _databaseContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());

        if (!_databaseContext.Roles.Any()) 
            _databaseContext.Roles.AddRange(RolesSeeder.SeedRoles());

        if (!_databaseContext.Permissions.Any()) 
            _databaseContext.Permissions.AddRange(PermissionsSeeder.SeedPermissions());

        if (!_databaseContext.DefaultPermissions.Any()) 
            _databaseContext.DefaultPermissions.AddRange(DefaultPermissionsSeeder.SeedDefaultPermissions());

        if (!_databaseContext.UserPermissions.Any()) 
            _databaseContext.UserPermissions.AddRange(UserPermissionsSeeder.SeedUserPermissions());

        if (!_databaseContext.UserRoles.Any()) 
            _databaseContext.UserRoles.AddRange(UserRolesSeeder.SeedUserRoles());
            
        _databaseContext.SaveChanges();
    }
}