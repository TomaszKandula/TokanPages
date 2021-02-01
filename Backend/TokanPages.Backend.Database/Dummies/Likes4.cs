using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class Likes4
    {
        public static Guid Id = Guid.Parse("5779c8cd-14ac-4178-ac4a-6bebe402017c");
        public static Guid ArticleId = Dummies.Article3.Id;
        public static Guid? UserId = null;
        public static string IpAddress = "125.125.125.125";
        public static int LikeCount = 5;
    }
}
