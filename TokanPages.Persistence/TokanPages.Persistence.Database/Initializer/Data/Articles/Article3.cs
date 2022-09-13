using System;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.Database.Initializer.Data.Articles;

[ExcludeFromCodeCoverage]
public static class Article3
{
    public static readonly Guid Id = Guid.Parse("f6493f03-0e85-466c-970b-6f1a07001173");

    public const string Title = "SQL Injection";

    public const string Description = "This article will explore the issue in greater detail";

    public const bool IsPublished = true;

    public const int ReadCount = 0;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-12 22:01:33");

    public static readonly DateTime? UpdatedAt = null;

    public static readonly Guid UserId = User3.Id;
}