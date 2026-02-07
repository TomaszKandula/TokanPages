using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]//TODO: add attribute, remove EFCore nav props
public class DefaultPermission : Entity<Guid>, IAuditable
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Role Role { get; set; } 
    public Permission Permission { get; set; }
}