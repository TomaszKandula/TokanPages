namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserPermissions : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid PermissionId { get; set; }

    public Users UserNavigation { get; set; }

    public Permissions PermissionNavigation { get; set; }
}