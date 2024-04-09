using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class UserPermissions : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Users UserNavigation { get; set; }
    public Permissions PermissionNavigation { get; set; }
}