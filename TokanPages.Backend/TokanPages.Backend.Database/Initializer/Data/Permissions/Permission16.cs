namespace TokanPages.Backend.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Permission16
{
    public static readonly Guid Id = Guid.Parse("2f159e38-674d-40c2-9407-41d4a4752954");

    public static string Name => nameof(Identity.Authorization.Permissions.CanUpdatePhotoAlbums);
}