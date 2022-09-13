using System;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.Database.Initializer.Data.Articles;

[ExcludeFromCodeCoverage]
public static class Article2
{
    public static readonly Guid Id = Guid.Parse("7494688a-994c-4905-9073-8c68811ec839");

    public const string Title = "The history of Excel";

    public const string Description = "The most recognizable spreadsheet application in the World";

    public const bool IsPublished = false;

    public const int ReadCount = 0;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-25 05:09:19");

    public static readonly DateTime? UpdatedAt = null;

    public static readonly Guid UserId = User2.Id;
}