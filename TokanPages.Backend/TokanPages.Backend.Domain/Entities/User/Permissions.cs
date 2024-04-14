using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class Permissions : Entity<Guid>, IAuditable
{
    [MaxLength(100)]
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public ICollection<DefaultPermissions> DefaultPermissions { get; set; } = new HashSet<DefaultPermissions>();
    public ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>();
}