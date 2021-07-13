namespace TokanPages.Backend.Database.Initializer.Data.Articles
{   
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Users;

    [ExcludeFromCodeCoverage]
    public static class Article3
    {
        public const string TITLE = "Records in C# 9.0";

        public const string DESCRIPTION = "Deep dive...";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;
        
        public static readonly Guid FId = Guid.Parse("f6493f03-0e85-466c-970b-6f1a07001173");
        
        public static readonly DateTime FCreated = DateTime.Parse("2020-09-12 22:01:33");
        
        public static readonly DateTime? FLastUpdated = null;
        
        public static readonly Guid FUserId = User3.FId;
    }
}