﻿using System;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Database.Initializer.Data.Users;

namespace TokanPages.Backend.Database.Initializer.Data.Articles
{
    [ExcludeFromCodeCoverage]
    public static class Article2
    {
        public const string TITLE = "Say goodbay to PHP";

        public const string DESCRIPTION = "Use C# for everything...";
        
        public const bool IS_PUBLISHED = false;
        
        public const int READ_COUNT = 0;
        
        public static readonly Guid FId = Guid.Parse("7494688a-994c-4905-9073-8c68811ec839");
        
        public static readonly DateTime FCreated = DateTime.Parse("2020-01-25 05:09:19");
        
        public static readonly DateTime? FLastUpdated = null;
        
        public static readonly Guid FUserId = User2.FId;
    }
}
