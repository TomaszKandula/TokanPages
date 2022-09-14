using TokanPages.Services.UserService.Models;

namespace TokanPages.Backend.Application.Users.Commands;

public class AuthenticateUserCommandResult
{
    public Guid UserId { get; set; }

    public string? AliasName { get; set; }

    public string? AvatarName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? ShortBio { get; set; }

    public DateTime Registered { get; set; }

    public string UserToken { get; set; } = "";

    public string RefreshToken { get; set; } = "";

    public List<GetUserRolesOutput> Roles { get; set; } = new();

    public List<GetUserPermissionsOutput> Permissions { get; set; } = new();
}