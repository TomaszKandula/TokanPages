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

    public ICollection<DefaultPermissions> DefaultPermissionsNavigation { get; set; } = new HashSet<DefaultPermissions>();

    public ICollection<UserPermissions> UserPermissionsNavigation { get; set; } = new HashSet<UserPermissions>();
}