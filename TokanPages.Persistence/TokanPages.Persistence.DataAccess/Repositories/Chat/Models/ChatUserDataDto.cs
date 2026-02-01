using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Chat.Models;

[ExcludeFromCodeCoverage]
public class ChatUserDataDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserImageName { get; set; } = string.Empty;
}