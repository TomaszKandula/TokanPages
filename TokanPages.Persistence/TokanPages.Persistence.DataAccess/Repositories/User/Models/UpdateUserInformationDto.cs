using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserInformationDto
{
    public required Guid UserId { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string UserAboutText { get; init; }

    public required string UserImageName { get; init; }

    public required string UserVideoName { get; init; }
}