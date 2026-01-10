using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class Permission : Entity<Guid>, IAuditable
{
    [MaxLength(100)]
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public ICollection<DefaultPermission> DefaultPermissions { get; set; } = new HashSet<DefaultPermission>();
    public ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>();
}