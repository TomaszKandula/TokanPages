﻿using System;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Database.Initializer.Data.Articles;

namespace TokanPages.Backend.Database.Initializer.Data.ArticleLikes
{
    [ExcludeFromCodeCoverage]
    public static class ArticleLike2
    {
        public const string IP_ADDRESS = "127.0.0.255";
        
        public const int LIKE_COUNT = 10;
        
        public static readonly Guid FId = Guid.Parse("59ebd0f9-a8b7-4d85-b863-064a4641fe90");
        
        public static readonly Guid FArticleId = Article2.FId;
        
        public static readonly Guid? FUserId = null;
    }
}
