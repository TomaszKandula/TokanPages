using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class UserInfo : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }
    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }
    [MaxLength(255)]
    public string UserAboutText { get; set; }
    [MaxLength(255)]
    public string UserImageName { get; set; }
    [MaxLength(255)]
    public string UserVideoName { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Users UserNavigation { get; set; }
}