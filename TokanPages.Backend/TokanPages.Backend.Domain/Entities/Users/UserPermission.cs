using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
public class UserPermission : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public User User { get; set; }
    public Permission Permission { get; set; }
}