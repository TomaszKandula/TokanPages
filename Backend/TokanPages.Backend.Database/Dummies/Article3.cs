using System;

namespace TokanPages.Backend.Database.Dummies
{   
    public static class Article3
    {
        public const string TITLE = "Records in C# 9.0";

        public const string DESCRIPTION = "Deep dive...";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;
        
        public static Guid FId = Guid.Parse("f6493f03-0e85-466c-970b-6f1a07001173");
        
        public static DateTime FCreated = DateTime.Parse("2020-09-12 22:01:33");
        
        public static DateTime? FLastUpdated = null;
        
        public static Guid FUserId = User3.FId;
    }
}
