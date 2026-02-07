using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserTokens")]
public class UserToken : Entity<Guid>
{
    public Guid UserId { get; set; }

    public string Token { get; set; }

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; }

    public string CreatedByIp { get; set; }

    public string Command { get; set; }

    public DateTime? Revoked { get; set; }

    public string RevokedByIp { get; set; }

    public string ReasonRevoked { get; set; }
}