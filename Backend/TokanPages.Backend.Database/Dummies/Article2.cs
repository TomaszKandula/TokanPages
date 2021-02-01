﻿using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class Article2
    {
        public static Guid Id = Guid.Parse("7494688a-994c-4905-9073-8c68811ec839");
        public static string Title = "Say goodbay to PHP";
        public static string Description = "Use C# for everything...";
        public static DateTime Created = DateTime.Parse("2020-01-25 05:09:19");
        public static DateTime? LastUpdated = null;
        public static bool IsPublished = false;
        public static int ReadCount = 0;
        public static Guid UserId = User2.Id;
    }
}
