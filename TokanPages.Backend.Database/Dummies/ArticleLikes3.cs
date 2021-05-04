using System;

namespace TokanPages.Backend.Database.Dummies
{
    public static class ArticleLikes3
    {
        public const string IP_ADDRESS = "255.255.255.255";

        public const int LIKE_COUNT = 10;
        
        public static Guid FId = Guid.Parse("f786f9b8-f391-43e5-af1a-f2d5004006b5");
        
        public static Guid FArticleId = Article3.FId;
        
        public static Guid? FUserId = null;
    }
}
