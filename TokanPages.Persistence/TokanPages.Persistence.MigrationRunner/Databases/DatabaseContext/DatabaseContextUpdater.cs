using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Seeders;
using TokanPages.Persistence.MigrationRunner.Helpers;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext;

[ExcludeFromCodeCoverage]
public static class DatabaseContextUpdater
{
    private const string Caller = nameof(DatabaseContextUpdater);

    public static async Task UpdateProduction(string sourceConnection, string targetConnection)
    {
        ConsolePrints.PrintOnInfo($"[{Caller}]: Working on the target connection...");

        var sourceDatabase = DatabaseConnection.GetDatabaseName(sourceConnection);
        var targetDatabase = DatabaseConnection.GetDatabaseName(targetConnection);

        var options = DatabaseOptions.GetOptions<Database.Contexts.OperationsDbContext>(targetConnection);
        var context = new Database.Contexts.OperationsDbContext(options);
        await context.Database.OpenConnectionAsync();
        var command = context.Database.GetDbConnection().CreateCommand();

        command.CommandText = DatabaseUpdate.GetSqlScript(DatabaseUpdate.DefaultUserScript);
        command.CommandTimeout = 90;
        var createResult = await command.ExecuteNonQueryAsync();

        ConsolePrints.PrintOnSuccess($"[{Caller}]: Default user created. Returned: {createResult}.");

        var version = DatabaseConnection.GetNextVersion(new SqlConnectionStringBuilder(sourceConnection));
        var scriptName = DatabaseUpdate.BuildMigrationScriptName(version.number, nameof(Database.Contexts.OperationsDbContext));
        var scriptContent = DatabaseUpdate.GetSqlScript(scriptName);

        scriptContent = scriptContent.Replace("{{SOURCE_TABLE}}", sourceDatabase);
        scriptContent = scriptContent.Replace("{{TARGET_TABLE}}", targetDatabase);

        command.CommandText = scriptContent;
        command.CommandTimeout = 240;
        var copyResult = await command.ExecuteNonQueryAsync();

        ConsolePrints.PrintOnSuccess($"[{Caller}]: Database copied. Returned: {copyResult}.");
    }

    public static void PopulateTestData(Database.Contexts.OperationsDbContext operationsDbContext)
    {
        if (!operationsDbContext.Users.Any())
        {
            operationsDbContext.Users.AddRange(UsersSeeder.SeedUsers());
            PrintInfo(UsersSeeder.SeedUsers().Count(), nameof(operationsDbContext.Users));
        }

        if (!operationsDbContext.UserInformation.Any())
        {
            operationsDbContext.UserInformation.AddRange(UserInfoSeeder.SeedUserInfo());
            PrintInfo(UserInfoSeeder.SeedUserInfo().Count(), nameof(operationsDbContext.UserInformation));
        }

        if (!operationsDbContext.Articles.Any())
        {
            operationsDbContext.Articles.AddRange(ArticlesSeeder.SeedArticles());
            PrintInfo(ArticlesSeeder.SeedArticles().Count(), nameof(operationsDbContext.Articles));
        }

        if (!operationsDbContext.Newsletters.Any())
        {
            operationsDbContext.Newsletters.AddRange(SubscribersSeeder.SeedSubscribers());
            PrintInfo(SubscribersSeeder.SeedSubscribers().Count(), nameof(operationsDbContext.Newsletters));
        }

        if (!operationsDbContext.ArticleLikes.Any())
        {
            operationsDbContext.ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());
            PrintInfo(ArticleLikesSeeder.SeedArticleLikes().Count(), nameof(operationsDbContext.ArticleLikes));
        }

        if (!operationsDbContext.PhotoCategories.Any())
        {
            operationsDbContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());
            PrintInfo(PhotoCategoriesSeeder.SeedPhotoCategories().Count(), nameof(operationsDbContext.PhotoCategories));
        }

        if (!operationsDbContext.Roles.Any())
        {
            operationsDbContext.Roles.AddRange(RolesSeeder.SeedRoles());
            PrintInfo(RolesSeeder.SeedRoles().Count(), nameof(operationsDbContext.Roles));
        }

        if (!operationsDbContext.Permissions.Any())
        {
            operationsDbContext.Permissions.AddRange(PermissionsSeeder.SeedPermissions());
            PrintInfo(PermissionsSeeder.SeedPermissions().Count(), nameof(operationsDbContext.Permissions));
        }

        if (!operationsDbContext.DefaultPermissions.Any())
        {
            operationsDbContext.DefaultPermissions.AddRange(DefaultPermissionsSeeder.SeedDefaultPermissions());
            PrintInfo(DefaultPermissionsSeeder.SeedDefaultPermissions().Count(), nameof(operationsDbContext.DefaultPermissions));
        }

        if (!operationsDbContext.UserPermissions.Any())
        {
            operationsDbContext.UserPermissions.AddRange(UserPermissionsSeeder.SeedUserPermissions());
            PrintInfo(UserPermissionsSeeder.SeedUserPermissions().Count(), nameof(operationsDbContext.UserPermissions));
        }

        if (!operationsDbContext.UserRoles.Any())
        {
            operationsDbContext.UserRoles.AddRange(UserRolesSeeder.SeedUserRoles());
            PrintInfo(UserRolesSeeder.SeedUserRoles().Count(), nameof(operationsDbContext.UserRoles));
        }

        operationsDbContext.SaveChanges();
        ConsolePrints.PrintOnSuccess($"[{Caller}]: Changes saved!");
    }

    public static void RemoveTestData(Database.Contexts.OperationsDbContext operationsDbContext)
    {
        operationsDbContext.RemoveRange(operationsDbContext.Albums);
        PrintWarning(nameof(operationsDbContext.Albums));
        operationsDbContext.RemoveRange(operationsDbContext.HttpRequests);
        PrintWarning(nameof(operationsDbContext.HttpRequests));
        operationsDbContext.RemoveRange(operationsDbContext.Newsletters);
        PrintWarning(nameof(operationsDbContext.Newsletters));
        operationsDbContext.RemoveRange(operationsDbContext.DefaultPermissions);
        PrintWarning(nameof(operationsDbContext.DefaultPermissions));

        operationsDbContext.RemoveRange(operationsDbContext.ArticleCounts);
        PrintWarning(nameof(operationsDbContext.ArticleCounts));
        operationsDbContext.RemoveRange(operationsDbContext.ArticleLikes);
        PrintWarning(nameof(operationsDbContext.ArticleLikes));
        operationsDbContext.RemoveRange(operationsDbContext.Articles);
        PrintWarning(nameof(operationsDbContext.Articles));

        operationsDbContext.RemoveRange(operationsDbContext.PhotoCategories);
        PrintWarning(nameof(operationsDbContext.PhotoCategories));
        operationsDbContext.RemoveRange(operationsDbContext.PhotoGears);
        PrintWarning(nameof(operationsDbContext.PhotoGears));
        operationsDbContext.RemoveRange(operationsDbContext.UserPhotos);
        PrintWarning(nameof(operationsDbContext.UserPhotos));

        operationsDbContext.RemoveRange(operationsDbContext.UserPermissions);
        PrintWarning(nameof(operationsDbContext.UserPermissions));
        operationsDbContext.RemoveRange(operationsDbContext.UserRoles);
        PrintWarning(nameof(operationsDbContext.UserRoles));
        operationsDbContext.RemoveRange(operationsDbContext.UserRefreshTokens);
        PrintWarning(nameof(operationsDbContext.UserRefreshTokens));
        operationsDbContext.RemoveRange(operationsDbContext.UserTokens);
        PrintWarning(nameof(operationsDbContext.UserTokens));
        operationsDbContext.RemoveRange(operationsDbContext.UserInformation);
        PrintWarning(nameof(operationsDbContext.UserInformation));
        operationsDbContext.RemoveRange(operationsDbContext.Users);
        PrintWarning(nameof(operationsDbContext.Users));

        operationsDbContext.RemoveRange(operationsDbContext.Permissions);
        PrintWarning(nameof(operationsDbContext.Permissions));
        operationsDbContext.RemoveRange(operationsDbContext.Roles);
        PrintWarning(nameof(operationsDbContext.Roles));

        operationsDbContext.SaveChanges();
        ConsolePrints.PrintOnSuccess($"[{Caller}]: Changes saved!");
    }

    private static void PrintInfo(int count, string entity)
        => ConsolePrints.PrintOnInfo($"[{Caller}]: Adding {count} entries to the '{entity}' table...");

    private static void PrintWarning(string entity)
        => ConsolePrints.PrintOnWarning($"[{Caller}]: '{entity}' is marked for removal...");
}