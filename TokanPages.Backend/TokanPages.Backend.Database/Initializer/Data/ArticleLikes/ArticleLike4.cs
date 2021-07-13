namespace TokanPages.Backend.Database.Initializer.Data.ArticleLikes
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Articles;

    [ExcludeFromCodeCoverage]
    public static class ArticleLike4
    {
        public const string IP_ADDRESS = "127.0.0.125";

        public const int LIKE_COUNT = 5;

        public static readonly Guid FId = Guid.Parse("5779c8cd-14ac-4178-ac4a-6bebe402017c");
        
        public static readonly Guid FArticleId = Article3.FId;
        
        public static readonly Guid? FUserId = null;
    }
}