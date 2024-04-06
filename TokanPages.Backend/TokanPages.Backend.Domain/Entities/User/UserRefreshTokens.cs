using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class UserRefreshTokens : Entity<Guid>
{
    public Guid UserId { get; set; }
    [Required]
    [MaxLength(500)]
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    [Required]
    [MaxLength(15)]
    public string CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; }
    [MaxLength(15)]
    public string RevokedByIp { get; set; }
    [MaxLength(500)]
    public string ReplacedByToken { get; set; }
    [MaxLength(255)]
    public string ReasonRevoked { get; set; }

    /* Navigation properties */
    public Users UserNavigation { get; set; }
}