using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]//TODO: remove EFCore nav props
[DatabaseTable(Schema = "operation", TableName = "UserPermissions")]
public class UserPermission : Entity<Guid>, IAuditable
{
    public required Guid UserId { get; set; }

    public required Guid PermissionId { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Permission Permission { get; set; }
}