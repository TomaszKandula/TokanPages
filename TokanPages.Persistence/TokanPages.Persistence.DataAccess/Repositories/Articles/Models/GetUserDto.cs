using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class GetUserDto
{
    public required Guid UserId { get; init; }

    public string? AliasName { get; init; }

    public string? AvatarName { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Email { get; init; }

    public string? ShortBio { get; init; }

    public required DateTime Registered { get; init; }
}