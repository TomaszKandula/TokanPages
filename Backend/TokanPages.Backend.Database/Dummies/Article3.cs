using System;

namespace TokanPages.Backend.Database.Dummies
{   
    public class Article3
    {
        public static Guid Id = Guid.Parse("f6493f03-0e85-466c-970b-6f1a07001173");
        public static string Title = "Records in C# 9.0";
        public static string Description = "Deep dive...";
        public static DateTime Created = DateTime.Parse("2020-09-12 22:01:33");
        public static DateTime? LastUpdated = null;
        public static bool IsPublished = true;
        public static int Likes = 0;
        public static int ReadCount = 0;
        public static Guid UserId = User3.Id;
    }
}
