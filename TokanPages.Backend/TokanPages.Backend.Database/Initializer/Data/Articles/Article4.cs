﻿namespace TokanPages.Backend.Database.Initializer.Data.Articles
{   
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Users;

    [ExcludeFromCodeCoverage]
    public static class Article4
    {
        public const string TITLE = ".NET Memory Management";

        public const string DESCRIPTION = "The basics you should know";
        
        public const bool IS_PUBLISHED = true;
        
        public const int READ_COUNT = 0;
        
        public static readonly Guid FId = Guid.Parse("d797cf99-a993-43e5-a71e-ad6a0791b56d");
        
        public static readonly DateTime FCreated = DateTime.Parse("2020-09-30 12:01:33");
        
        public static readonly DateTime? FLastUpdated = null;
        
        public static readonly Guid FUserId = User3.FId;
    }
}