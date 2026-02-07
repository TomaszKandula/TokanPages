using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]//TODO: add attribute, remove EFCore nav props
public class UserToken : Entity<Guid>
{
    public Guid UserId { get; set; }
    [Required]
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    [Required]
    [MaxLength(15)]
    public string CreatedByIp { get; set; }
    [Required]
    [MaxLength(255)]
    public string Command { get; set; }
    public DateTime? Revoked { get; set; }
    [MaxLength(15)]
    public string RevokedByIp { get; set; }
    [MaxLength(255)]
    public string ReasonRevoked { get; set; }

    /* Navigation properties */
    public User User { get; set; }
}