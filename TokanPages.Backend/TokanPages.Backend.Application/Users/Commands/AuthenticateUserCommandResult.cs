namespace TokanPages.Backend.Application.Handlers.Commands.Users;

using System.Collections.Generic;
using WebApi.Dto.Users;

public class AuthenticateUserCommandResult : GetUserDto
{
    public string UserToken { get; set; } = "";

    public string RefreshToken { get; set; } = "";

    public List<GetUserRoleDto> Roles { get; set; } = new();

    public List<GetUserPermissionDto> Permissions { get; set; } = new();
}