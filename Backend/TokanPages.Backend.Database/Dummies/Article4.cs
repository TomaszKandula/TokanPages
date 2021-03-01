using System;

namespace TokanPages.Backend.Database.Dummies
{   
    public class Article4
    {
        public static Guid Id = Guid.Parse("d797cf99-a993-43e5-a71e-ad6a0791b56d");
        public static string Title = "Java sucks! So PHP...";
        public static string Description = "Stay away from it...";
        public static DateTime Created = DateTime.Parse("2020-09-30 12:01:33");
        public static DateTime? LastUpdated = null;
        public static bool IsPublished = true;
        public static int ReadCount = 0;
        public static Guid UserId = User3.Id;
    }
}
