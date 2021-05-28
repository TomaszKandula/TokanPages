using System;

namespace TokanPages.Backend.Database.Initializer.Data
{
    public static class Article1
    {
        public const string TITLE = "Why C# is great?";

        public const string DESCRIPTION = "No JAVA needed anymore...";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;

        public static readonly Guid FId = Guid.Parse("731a6665-1c80-44e5-af6e-4d8331efe028");
        
        public static readonly DateTime FCreated = DateTime.Parse("2020-01-10 12:15:15");
        
        public static readonly DateTime? FLastUpdated = null;
        
        public static readonly Guid FUserId = User1.FId;
    }
}
