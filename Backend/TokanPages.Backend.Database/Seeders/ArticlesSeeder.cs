using System;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{

    public class ArticlesSeeder : IDatabaseContextSeeder
    {

        public class Dummy1
        {
            public static Guid Id = Guid.Parse("731a6665-1c80-44e5-af6e-4d8331efe028");
            public static string Title = "Why C# is great?";
            public static string Description = "No JAVA needed anymore...";
            public static DateTime Created = DateTime.Parse("2020-01-10 12:15:15");
            public static DateTime? LastUpdated = null;
            public static bool IsPublished = false;
            public static int Likes = 0;
            public static int ReadCount = 0;
        }

        public class Dummy2
        {
            public static Guid Id = Guid.Parse("7494688a-994c-4905-9073-8c68811ec839");
            public static string Title = "Say goodbay to PHP";
            public static string Description = "Use C# for everything...";
            public static DateTime Created = DateTime.Parse("2020-01-25 05:09:19");
            public static DateTime? LastUpdated = null;
            public static bool IsPublished = false;
            public static int Likes = 0;
            public static int ReadCount = 0;
        }

        public class Dummy3
        {
            public static Guid Id = Guid.Parse("f6493f03-0e85-466c-970b-6f1a07001173");
            public static string Title = "Records in C# 9.0";
            public static string Description = "Deep dive...";
            public static DateTime Created = DateTime.Parse("2020-09-12 22:01:33");
            public static DateTime? LastUpdated = null;
            public static bool IsPublished = false;
            public static int Likes = 0;
            public static int ReadCount = 0;
        }

        public void Seed(ModelBuilder AModelBuilder)
        {

            AModelBuilder.Entity<Articles>()
                .HasData(
                    new Articles 
                    { 
                        Id = Dummy1.Id,
                        Title = Dummy1.Title,
                        Description = Dummy1.Description,
                        IsPublished = Dummy1.IsPublished,
                        Likes = Dummy1.Likes,
                        ReadCount = Dummy1.ReadCount,
                        CreatedAt = Dummy1.Created,
                        UpdatedAt = Dummy1.LastUpdated
                    },
                    new Articles
                    {
                        Id = Dummy2.Id,
                        Title = Dummy2.Title,
                        Description = Dummy2.Description,
                        IsPublished = Dummy2.IsPublished,
                        Likes = Dummy2.Likes,
                        ReadCount = Dummy2.ReadCount,
                        CreatedAt = Dummy2.Created,
                        UpdatedAt = Dummy2.LastUpdated
                    },
                    new Articles
                    {
                        Id = Dummy3.Id,
                        Title = Dummy3.Title,
                        Description = Dummy3.Description,
                        IsPublished = Dummy3.IsPublished,
                        Likes = Dummy3.Likes,
                        ReadCount = Dummy3.ReadCount,
                        CreatedAt = Dummy3.Created,
                        UpdatedAt = Dummy3.LastUpdated
                    }
                );

        }

    }

}
