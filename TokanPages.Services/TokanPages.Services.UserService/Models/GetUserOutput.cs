using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class GetUserOutput
{
    public Guid UserId { get; set; }

    public string? AliasName { get; set; }

    public string? AvatarName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? ShortBio { get; set; }

    public DateTime Registered { get; set; }
}