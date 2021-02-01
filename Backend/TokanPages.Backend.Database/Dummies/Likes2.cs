using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class Likes2
    {
        public static Guid Id = Guid.Parse("59ebd0f9-a8b7-4d85-b863-064a4641fe90");
        public static Guid ArticleId = Dummies.Article2.Id;
        public static Guid? UserId = null;
        public static string IpAddress = "255.255.255.255";
        public static int LikeCount = 10;
    }
}
