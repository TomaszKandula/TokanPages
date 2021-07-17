namespace TokanPages.Backend.Database.Initializer.Data.ArticleLikes
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Articles;

    [ExcludeFromCodeCoverage]
    public static class ArticleLike3
    {
        public const string IP_ADDRESS = "127.0.0.255";

        public const int LIKE_COUNT = 10;
        
        public static readonly Guid FId = Guid.Parse("f786f9b8-f391-43e5-af1a-f2d5004006b5");
        
        public static readonly Guid FArticleId = Article3.FId;
        
        public static readonly Guid? FUserId = null;
    }
}