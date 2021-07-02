using System;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Database.Initializer.Data.Users;

namespace TokanPages.Backend.Database.Initializer.Data.Articles
{   
    [ExcludeFromCodeCoverage]
    public static class Article4
    {
        public const string TITLE = "Java sucks! So PHP...";

        public const string DESCRIPTION = "Stay away from it...";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;
        
        public static readonly Guid FId = Guid.Parse("d797cf99-a993-43e5-a71e-ad6a0791b56d");
        
        public static readonly DateTime FCreated = DateTime.Parse("2020-09-30 12:01:33");
        
        public static readonly DateTime? FLastUpdated = null;
        
        public static readonly Guid FUserId = User3.FId;
    }
}
