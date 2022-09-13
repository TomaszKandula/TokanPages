using System.Collections.Generic;
using TokanPages.WebApi.Dto.Users;

namespace TokanPages.Backend.Application.Users.Commands;

public class AuthenticateUserCommandResult : GetUserDto
{
    public string UserToken { get; set; } = "";

    public string RefreshToken { get; set; } = "";

    public List<GetUserRoleDto> Roles { get; set; } = new();

    public List<GetUserPermissionDto> Permissions { get; set; } = new();
}