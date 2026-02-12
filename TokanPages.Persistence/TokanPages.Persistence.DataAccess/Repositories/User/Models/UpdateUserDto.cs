using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    public required Guid UserId { get; set; }

    public required string UserAlias { get; set; }

    public required string EmailAddress { get; set; }

    public required bool IsActivated { get; set; }

    public required bool IsVerified { get; set; }
}