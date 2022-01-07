namespace TokanPages.Backend.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission11
{
    public static readonly Guid Id = Guid.Parse("a91cf136-0bec-428a-805b-6517cfae3f42");

    public static string Name => nameof(Permissions.CanInsertPhotos);
}