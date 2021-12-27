namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Collections.Generic;
using Shared.Dto.Users;

public class AuthenticateUserCommandResult : GetUserDto
{
    public string UserToken { get; set; }

    public List<GetUserRoleDto> Roles { get; set; }

    public List<GetUserPermissionDto> Permissions { get; set; }
}