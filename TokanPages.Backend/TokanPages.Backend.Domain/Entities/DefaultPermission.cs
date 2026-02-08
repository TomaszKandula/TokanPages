using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]//TODO: add attribute, remove EFCore nav props
public class DefaultPermission : Entity<Guid>, IAuditable
{
    public required Guid RoleId { get; set; }
    public required Guid PermissionId { get; set; }
    public required Guid CreatedBy { get; set; }
    public required DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Role Role { get; set; } 
    public Permission Permission { get; set; }
}