using System;

namespace TokanPages.Backend.Database.Dummies
{   
    public static class Article4
    {
        public const string TITLE = "Java sucks! So PHP...";

        public const string DESCRIPTION = "Stay away from it...";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;
        
        public static Guid FId = Guid.Parse("d797cf99-a993-43e5-a71e-ad6a0791b56d");
        
        public static DateTime FCreated = DateTime.Parse("2020-09-30 12:01:33");
        
        public static DateTime? FLastUpdated = null;
        
        public static Guid FUserId = User3.FId;
    }
}
