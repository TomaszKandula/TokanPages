using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Articles;

namespace TokanPages.Persistence.Database.Initializer.Data.ArticleLikes;

[ExcludeFromCodeCoverage]
public static class ArticleLike3
{
    public static readonly Guid Id = Guid.Parse("f786f9b8-f391-43e5-af1a-f2d5004006b5");

    public static readonly Guid ArticleId = Article3.Id;

    public static readonly Guid? UserId = null;

    public const string IpAddress = "127.0.0.255";

    public const int LikeCount = 10;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-10-01 10:15:11");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}