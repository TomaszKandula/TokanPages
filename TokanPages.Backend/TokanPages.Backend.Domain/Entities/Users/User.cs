using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Users")]
public class User : Entity<Guid>, IAuditable, ISoftDelete
{
    public required string UserAlias { get; set; }

    public required string EmailAddress { get; set; }

    public required string CryptedPassword { get; set; }

    public Guid? ResetId { get; set; }

    public DateTime? ResetIdEnds { get; set; }

    public Guid? ActivationId { get; set; }

    public DateTime? ActivationIdEnds { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public required bool IsActivated { get; set; }

    public required bool IsVerified { get; set; }

    public required bool IsDeleted { get; set; }

    public required bool HasBusinessLock { get; set; }
}