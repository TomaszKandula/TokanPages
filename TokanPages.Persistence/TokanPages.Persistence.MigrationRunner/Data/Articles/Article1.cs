using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Data.Articles;

[ExcludeFromCodeCoverage]
public static class Article1
{
    public static readonly Guid Id = Guid.Parse("731a6665-1c80-44e5-af6e-4d8331efe028");

    public const string Title = "I said goodbye to Stored Procedures";

    public const string Description = "In this article, I will explain why I do not need them that much";

    public const bool IsPublished = true;

    public const int ReadCount = 0;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-10 12:15:15");

    public static readonly DateTime? UpdatedAt = null;

    public static readonly Guid UserId = User1.Id;
}