using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Articles;

[ExcludeFromCodeCoverage]
public static class Article5
{
    public static readonly Guid Id = Guid.Parse("3cd2a36c-a0ed-4ea0-8ddc-21bcbc58f2cd");

    public const string Title = "SonarQube on Azure";

    public const string Description = "Continuous Inspection made easy";

    public const bool IsPublished = true;

    public const int ReadCount = 0;

    public static readonly DateTime Created = DateTime.Parse("2021-07-05 18:01:33");

    public static readonly DateTime? LastUpdated = null;

    public static readonly Guid UserId = User2.Id;
}