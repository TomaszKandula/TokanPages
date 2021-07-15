namespace TokanPages.Backend.Database.Initializer.Data.ArticleLikes
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Articles;
    using Users;

    [ExcludeFromCodeCoverage]
    public static class ArticleLike1
    {
        public const string IP_ADDRESS = "127.0.0.1";

        public const int LIKE_COUNT = 20;

        public static readonly Guid FId = Guid.Parse("79d08bf0-05fc-4064-af4a-e92cfd6acda8");
        
        public static readonly Guid FArticleId = Article1.FId;
        
        public static readonly Guid? FUserId = User1.FId;
    }
}