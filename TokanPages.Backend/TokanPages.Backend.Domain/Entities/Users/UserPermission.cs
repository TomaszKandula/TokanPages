using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]//TODO: remove EFCore nav props
[DatabaseTable(Schema = "operation", TableName = "UserPermissions")]
public class UserPermission : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }

    public Guid PermissionId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Permission Permission { get; set; }
}