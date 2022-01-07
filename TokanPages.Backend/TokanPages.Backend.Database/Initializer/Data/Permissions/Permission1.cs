namespace TokanPages.Backend.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission1
{
    public static readonly Guid Id = Guid.Parse("7e041e7f-c7bb-486b-9b09-a931015c36fd");
        
    public static string Name => nameof(Permissions.CanSelectArticles);
}