namespace TokanPages.Backend.Database.Initializer.Data.Articles
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Users;

    [ExcludeFromCodeCoverage]
    public static class Article2
    {
        public const string TITLE = "The history of Excel";

        public const string DESCRIPTION = "The most recognizable spreadsheet application in the World";
        
        public const bool IS_PUBLISHED = false;
        
        public const int READ_COUNT = 0;
        
        public static readonly Guid FId = Guid.Parse("7494688a-994c-4905-9073-8c68811ec839");
        
        public static readonly DateTime FCreated = DateTime.Parse("2020-01-25 05:09:19");
        
        public static readonly DateTime? FLastUpdated = null;
        
        public static readonly Guid FUserId = User2.FId;
    }
}