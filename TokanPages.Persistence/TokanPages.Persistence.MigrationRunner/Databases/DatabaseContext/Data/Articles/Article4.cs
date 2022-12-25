using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Data.Articles;

[ExcludeFromCodeCoverage]
public static class Article4
{
    public static readonly Guid Id = Guid.Parse("d797cf99-a993-43e5-a71e-ad6a0791b56d");

    public const string Title = ".NET Memory Management";

    public const string Description = "The basics you should know";

    public const bool IsPublished = true;

    public const int ReadCount = 0;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-30 12:01:33");

    public static readonly DateTime? UpdatedAt = null;

    public static readonly Guid UserId = User3.Id;
}