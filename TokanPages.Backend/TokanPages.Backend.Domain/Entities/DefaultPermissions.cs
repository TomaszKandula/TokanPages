namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class DefaultPermissions : Entity<Guid>
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public Roles RoleNavigation { get; set; } 

    public Permissions PermissionNavigation { get; set; }
}