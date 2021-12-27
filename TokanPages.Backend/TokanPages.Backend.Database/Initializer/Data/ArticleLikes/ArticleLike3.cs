namespace TokanPages.Backend.Database.Initializer.Data.ArticleLikes;

using System;
using System.Diagnostics.CodeAnalysis;
using Articles;

[ExcludeFromCodeCoverage]
public static class ArticleLike3
{
    public const string IpAddress = "127.0.0.255";

    public const int LikeCount = 10;
        
    public static readonly Guid Id = Guid.Parse("f786f9b8-f391-43e5-af1a-f2d5004006b5");
        
    public static readonly Guid ArticleId = Article3.Id;
        
    public static readonly Guid? UserId = null;
}