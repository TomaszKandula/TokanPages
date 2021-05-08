using System;

namespace TokanPages.Backend.Database.Initializer.Data
{
    public static class ArticleLikes1
    {
        public const string IP_ADDRESS = "1.1.1.1";

        public const int LIKE_COUNT = 20;

        public static Guid FId = Guid.Parse("79d08bf0-05fc-4064-af4a-e92cfd6acda8");
        
        public static Guid FArticleId = Article1.FId;
        
        public static Guid? FUserId = User1.FId;
    }
}
