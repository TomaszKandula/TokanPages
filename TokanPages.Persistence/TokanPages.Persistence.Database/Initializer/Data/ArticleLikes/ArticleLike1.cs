using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Articles;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.Database.Initializer.Data.ArticleLikes;

[ExcludeFromCodeCoverage]
public static class ArticleLike1
{
    public static readonly Guid Id = Guid.Parse("79d08bf0-05fc-4064-af4a-e92cfd6acda8");

    public static readonly Guid ArticleId = Article1.Id;

    public static readonly Guid? UserId = User1.Id;

    public const string IpAddress = "127.0.0.1";

    public const int LikeCount = 20;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-10-01 10:15:11");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}