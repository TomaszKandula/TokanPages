using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class ArticleLikes1
    {
        public static Guid Id = Guid.Parse("79d08bf0-05fc-4064-af4a-e92cfd6acda8");
        public static Guid ArticleId = Article1.Id;
        public static Guid? UserId = User1.Id;
        public static string IpAddress = "1.1.1.1";
        public static int LikeCount = 20;    
    }
}
