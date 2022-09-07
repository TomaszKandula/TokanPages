namespace TokanPages.Persistence.Database.Initializer.Data.ArticleLikes;

using System;
using System.Diagnostics.CodeAnalysis;
using Articles;

[ExcludeFromCodeCoverage]
public static class ArticleLike4
{
    public const string IpAddress = "127.0.0.125";

    public const int LikeCount = 5;

    public static readonly Guid Id = Guid.Parse("5779c8cd-14ac-4178-ac4a-6bebe402017c");

    public static readonly Guid ArticleId = Article3.Id;

    public static readonly Guid? UserId = null;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-10-01 10:15:11");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}