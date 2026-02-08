using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]//TODO: remove EFCore nav props
[DatabaseTable(Schema = "operation", TableName = "UserRoles")]
public class UserRole : Entity<Guid>, IAuditable
{
    public required Guid UserId { get; set; }

    public required Guid RoleId { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public User User { get; set; }
    public Role Role { get; set; }
}