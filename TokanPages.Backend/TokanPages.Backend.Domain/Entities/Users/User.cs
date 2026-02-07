using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Users")]
public class User : Entity<Guid>, IAuditable, ISoftDelete
{
    [Required]
    [MaxLength(255)]
    public string UserAlias { get; set; }
    [Required]
    [MaxLength(255)]
    public string EmailAddress { get; set; }
    [Required]
    [MaxLength(100)]
    public string CryptedPassword { get; set; }
    public Guid? ResetId { get; set; }
    public DateTime? ResetIdEnds { get; set; }
    public Guid? ActivationId { get; set; }
    public DateTime? ActivationIdEnds { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsActivated { get; set; }
    public bool IsVerified { get; set; }
    public bool IsDeleted { get; set; }
    public bool HasBusinessLock { get; set; }
}