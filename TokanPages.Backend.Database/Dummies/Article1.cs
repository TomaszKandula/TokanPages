using System;

namespace TokanPages.Backend.Database.Dummies
{
    public static class Article1
    {
        public const string TITLE = "Why C# is great?";

        public const string DESCRIPTION = "No JAVA needed anymore...";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;

        public static Guid FId = Guid.Parse("731a6665-1c80-44e5-af6e-4d8331efe028");
        
        public static DateTime FCreated = DateTime.Parse("2020-01-10 12:15:15");
        
        public static DateTime? FLastUpdated = null;
        
        public static Guid FUserId = User1.FId;
    }
}
