namespace TokanPages.Backend.Database.Initializer.Data.Articles;

using System;
using System.Diagnostics.CodeAnalysis;
using Users;

[ExcludeFromCodeCoverage]
public static class Article4
{
    public const string Title = ".NET Memory Management";

    public const string Description = "The basics you should know";
        
    public const bool IsPublished = true;
        
    public const int ReadCount = 0;
        
    public static readonly Guid Id = Guid.Parse("d797cf99-a993-43e5-a71e-ad6a0791b56d");
        
    public static readonly DateTime Created = DateTime.Parse("2020-09-30 12:01:33");
        
    public static readonly DateTime? LastUpdated = null;
        
    public static readonly Guid UserId = User3.Id;
}