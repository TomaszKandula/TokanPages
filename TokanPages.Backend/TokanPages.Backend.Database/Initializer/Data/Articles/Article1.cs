namespace TokanPages.Backend.Database.Initializer.Data.Articles
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Users;

    [ExcludeFromCodeCoverage]
    public static class Article1
    {
        public const string TITLE = "I said goodbye to Stored Procedures";

        public const string DESCRIPTION = "In this article, I will explain why I do not need them that much";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;

        public static readonly Guid FId = Guid.Parse("731a6665-1c80-44e5-af6e-4d8331efe028");
        
        public static readonly DateTime FCreated = DateTime.Parse("2020-01-10 12:15:15");
        
        public static readonly DateTime? FLastUpdated = null;
        
        public static readonly Guid FUserId = User1.FId;
    }
}