using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class DefaultPermissions : Entity<Guid>, IAuditable
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Roles Roles { get; set; } 
    public Permissions Permissions { get; set; }
}