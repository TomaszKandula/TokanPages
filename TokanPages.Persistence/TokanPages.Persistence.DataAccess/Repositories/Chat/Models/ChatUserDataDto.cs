using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Chat.Models;

[ExcludeFromCodeCoverage]
public class ChatUserDataDto
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string UserImageName { get; init; }
}