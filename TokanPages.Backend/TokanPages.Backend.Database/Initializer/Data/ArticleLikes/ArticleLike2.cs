namespace TokanPages.Backend.Database.Initializer.Data.ArticleLikes;

using System;
using System.Diagnostics.CodeAnalysis;
using Articles;

[ExcludeFromCodeCoverage]
public static class ArticleLike2
{
    public const string IpAddress = "127.0.0.255";
        
    public const int LikeCount = 10;
        
    public static readonly Guid Id = Guid.Parse("59ebd0f9-a8b7-4d85-b863-064a4641fe90");
        
    public static readonly Guid ArticleId = Article2.Id;
        
    public static readonly Guid? UserId = null;
}