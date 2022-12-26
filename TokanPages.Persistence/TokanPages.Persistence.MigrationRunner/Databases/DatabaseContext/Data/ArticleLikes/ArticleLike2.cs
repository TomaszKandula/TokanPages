using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Articles;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.ArticleLikes;

[ExcludeFromCodeCoverage]
public static class ArticleLike2
{
    public static readonly Guid Id = Guid.Parse("59ebd0f9-a8b7-4d85-b863-064a4641fe90");
        
    public static readonly Guid ArticleId = Article2.Id;
        
    public static readonly Guid? UserId = null;

    public const string IpAddress = "127.0.0.255";

    public const int LikeCount = 10;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-10-01 10:15:11");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}