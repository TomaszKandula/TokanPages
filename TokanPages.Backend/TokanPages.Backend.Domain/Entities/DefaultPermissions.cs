using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class DefaultPermissions : Entity<Guid>, IAuditable
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Roles RoleNavigation { get; set; } 

    public Permissions PermissionNavigation { get; set; }
}