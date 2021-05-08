using System;

namespace TokanPages.Backend.Database.Initializer.Data
{
    public static class Article2
    {
        public const string TITLE = "Say goodbay to PHP";

        public const string DESCRIPTION = "Use C# for everything...";
        
        public const bool IS_PUBLISHED = false;
        
        public const int READ_COUNT = 0;
        
        public static Guid FId = Guid.Parse("7494688a-994c-4905-9073-8c68811ec839");
        
        public static DateTime FCreated = DateTime.Parse("2020-01-25 05:09:19");
        
        public static DateTime? FLastUpdated = null;
        
        public static Guid FUserId = User2.FId;
    }
}
