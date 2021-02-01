using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class Likes3
    {
        public static Guid Id = Guid.Parse("f786f9b8-f391-43e5-af1a-f2d5004006b5");
        public static Guid ArticleId = Dummies.Article3.Id;
        public static Guid? UserId = null;
        public static string IpAddress = "255.255.255.255";
        public static int LikeCount = 10;
    }
}
