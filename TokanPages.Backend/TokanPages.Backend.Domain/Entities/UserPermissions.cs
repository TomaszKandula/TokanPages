namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using Contracts;

[ExcludeFromCodeCoverage]
public class UserPermissions : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }

    public Guid PermissionId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Users UserNavigation { get; set; }

    public Permissions PermissionNavigation { get; set; }
}