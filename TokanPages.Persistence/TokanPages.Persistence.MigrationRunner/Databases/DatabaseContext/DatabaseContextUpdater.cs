using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Shared.Services;
using TokanPages.Persistence.DataAccess.Contexts;
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

        var options = DatabaseOptions.GetOptions<OperationDbContext>(targetConnection);
        var context = new OperationDbContext(options);
        await context.Database.OpenConnectionAsync();
        var command = context.Database.GetDbConnection().CreateCommand();

        command.CommandText = DatabaseUpdate.GetSqlScript(DatabaseUpdate.DefaultUserScript);
        command.CommandTimeout = 90;
        var createResult = await command.ExecuteNonQueryAsync();

        ConsolePrints.PrintOnSuccess($"[{Caller}]: Default user created. Returned: {createResult}.");

        var version = DatabaseConnection.GetNextVersion(new SqlConnectionStringBuilder(sourceConnection));
        var scriptName = DatabaseUpdate.BuildMigrationScriptName(version.number, nameof(OperationDbContext));
        var scriptContent = DatabaseUpdate.GetSqlScript(scriptName);

        scriptContent = scriptContent.Replace("{{SOURCE_TABLE}}", sourceDatabase);
        scriptContent = scriptContent.Replace("{{TARGET_TABLE}}", targetDatabase);

        command.CommandText = scriptContent;
        command.CommandTimeout = 240;
        var copyResult = await command.ExecuteNonQueryAsync();

        ConsolePrints.PrintOnSuccess($"[{Caller}]: Database copied. Returned: {copyResult}.");
    }

    public static void PopulateTestData(OperationDbContext operationDbContext)
    {
        if (!operationDbContext.Users.Any())
        {
            operationDbContext.Users.AddRange(UsersSeeder.SeedUsers());
            PrintInfo(UsersSeeder.SeedUsers().Count(), nameof(operationDbContext.Users));
        }

        if (!operationDbContext.UserInformation.Any())
        {
            operationDbContext.UserInformation.AddRange(UserInfoSeeder.SeedUserInfo());
            PrintInfo(UserInfoSeeder.SeedUserInfo().Count(), nameof(operationDbContext.UserInformation));
        }

        if (!operationDbContext.Articles.Any())
        {
            operationDbContext.Articles.AddRange(ArticlesSeeder.SeedArticles());
            PrintInfo(ArticlesSeeder.SeedArticles().Count(), nameof(operationDbContext.Articles));
        }

        if (!operationDbContext.Newsletters.Any())
        {
            operationDbContext.Newsletters.AddRange(SubscribersSeeder.SeedSubscribers());
            PrintInfo(SubscribersSeeder.SeedSubscribers().Count(), nameof(operationDbContext.Newsletters));
        }

        if (!operationDbContext.ArticleLikes.Any())
        {
            operationDbContext.ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());
            PrintInfo(ArticleLikesSeeder.SeedArticleLikes().Count(), nameof(operationDbContext.ArticleLikes));
        }

        if (!operationDbContext.PhotoCategories.Any())
        {
            operationDbContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());
            PrintInfo(PhotoCategoriesSeeder.SeedPhotoCategories().Count(), nameof(operationDbContext.PhotoCategories));
        }

        if (!operationDbContext.Roles.Any())
        {
            operationDbContext.Roles.AddRange(RolesSeeder.SeedRoles());
            PrintInfo(RolesSeeder.SeedRoles().Count(), nameof(operationDbContext.Roles));
        }

        if (!operationDbContext.Permissions.Any())
        {
            operationDbContext.Permissions.AddRange(PermissionsSeeder.SeedPermissions());
            PrintInfo(PermissionsSeeder.SeedPermissions().Count(), nameof(operationDbContext.Permissions));
        }

        if (!operationDbContext.DefaultPermissions.Any())
        {
            operationDbContext.DefaultPermissions.AddRange(DefaultPermissionsSeeder.SeedDefaultPermissions());
            PrintInfo(DefaultPermissionsSeeder.SeedDefaultPermissions().Count(), nameof(operationDbContext.DefaultPermissions));
        }

        if (!operationDbContext.UserPermissions.Any())
        {
            operationDbContext.UserPermissions.AddRange(UserPermissionsSeeder.SeedUserPermissions());
            PrintInfo(UserPermissionsSeeder.SeedUserPermissions().Count(), nameof(operationDbContext.UserPermissions));
        }

        if (!operationDbContext.UserRoles.Any())
        {
            operationDbContext.UserRoles.AddRange(UserRolesSeeder.SeedUserRoles());
            PrintInfo(UserRolesSeeder.SeedUserRoles().Count(), nameof(operationDbContext.UserRoles));
        }

        operationDbContext.SaveChanges();
        ConsolePrints.PrintOnSuccess($"[{Caller}]: Changes saved!");
    }

    public static void RemoveTestData(OperationDbContext operationDbContext)
    {
        operationDbContext.RemoveRange(operationDbContext.Albums);
        PrintWarning(nameof(operationDbContext.Albums));
        operationDbContext.RemoveRange(operationDbContext.HttpRequests);
        PrintWarning(nameof(operationDbContext.HttpRequests));
        operationDbContext.RemoveRange(operationDbContext.Newsletters);
        PrintWarning(nameof(operationDbContext.Newsletters));
        operationDbContext.RemoveRange(operationDbContext.DefaultPermissions);
        PrintWarning(nameof(operationDbContext.DefaultPermissions));

        operationDbContext.RemoveRange(operationDbContext.ArticleCounts);
        PrintWarning(nameof(operationDbContext.ArticleCounts));
        operationDbContext.RemoveRange(operationDbContext.ArticleLikes);
        PrintWarning(nameof(operationDbContext.ArticleLikes));
        operationDbContext.RemoveRange(operationDbContext.Articles);
        PrintWarning(nameof(operationDbContext.Articles));

        operationDbContext.RemoveRange(operationDbContext.PhotoCategories);
        PrintWarning(nameof(operationDbContext.PhotoCategories));
        operationDbContext.RemoveRange(operationDbContext.PhotoGears);
        PrintWarning(nameof(operationDbContext.PhotoGears));
        operationDbContext.RemoveRange(operationDbContext.Photos);
        PrintWarning(nameof(operationDbContext.Photos));

        operationDbContext.RemoveRange(operationDbContext.UserPermissions);
        PrintWarning(nameof(operationDbContext.UserPermissions));
        operationDbContext.RemoveRange(operationDbContext.UserRoles);
        PrintWarning(nameof(operationDbContext.UserRoles));
        operationDbContext.RemoveRange(operationDbContext.UserRefreshTokens);
        PrintWarning(nameof(operationDbContext.UserRefreshTokens));
        operationDbContext.RemoveRange(operationDbContext.UserTokens);
        PrintWarning(nameof(operationDbContext.UserTokens));
        operationDbContext.RemoveRange(operationDbContext.UserInformation);
        PrintWarning(nameof(operationDbContext.UserInformation));
        operationDbContext.RemoveRange(operationDbContext.Users);
        PrintWarning(nameof(operationDbContext.Users));

        operationDbContext.RemoveRange(operationDbContext.Permissions);
        PrintWarning(nameof(operationDbContext.Permissions));
        operationDbContext.RemoveRange(operationDbContext.Roles);
        PrintWarning(nameof(operationDbContext.Roles));

        operationDbContext.SaveChanges();
        ConsolePrints.PrintOnSuccess($"[{Caller}]: Changes saved!");
    }

    private static void PrintInfo(int count, string entity)
        => ConsolePrints.PrintOnInfo($"[{Caller}]: Adding {count} entries to the '{entity}' table...");

    private static void PrintWarning(string entity)
        => ConsolePrints.PrintOnWarning($"[{Caller}]: '{entity}' is marked for removal...");
}