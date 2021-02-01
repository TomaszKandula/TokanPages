using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class Article1
    {
        public static Guid Id = Guid.Parse("731a6665-1c80-44e5-af6e-4d8331efe028");
        public static string Title = "Why C# is great?";
        public static string Description = "No JAVA needed anymore...";
        public static DateTime Created = DateTime.Parse("2020-01-10 12:15:15");
        public static DateTime? LastUpdated = null;
        public static bool IsPublished = true;
        public static int ReadCount = 0;
        public static Guid UserId = User1.Id;
    }
}
