namespace TokanPages.Persistence.Database.Initializer.Data.Roles;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Role1
{
    public static readonly Guid Id = Guid.Parse("413f8fc8-7f25-40e0-88f3-9f846288f6c5");

    public const string Name = nameof(Backend.Domain.Enums.Roles.GodOfAsgard);

    public const string Description = "Admin";

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-11-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}