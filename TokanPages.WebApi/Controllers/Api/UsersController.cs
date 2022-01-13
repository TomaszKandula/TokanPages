namespace TokanPages.WebApi.Controllers.Api;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Enums;
using Backend.Cqrs.Mappers;
using Backend.Shared.Dto.Users;
using Backend.Shared.Attributes;
using Backend.Cqrs.Handlers.Queries.Users;
using Backend.Cqrs.Handlers.Commands.Users;
using Services.Caching.Users;
using MediatR;

[Authorize]
[ApiVersion("1.0")]
public class UsersController : ApiBaseController
{
    private readonly IUsersCache _usersCache;

    public UsersController(IMediator mediator, IUsersCache usersCache) 
        : base(mediator) => _usersCache = usersCache;

    [HttpGet]
    [ProducesResponseType(typeof(GetUserVisitCountQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserVisitCountQueryResult> GetVisitCount()
        => await Mediator.Send(new GetUserVisitCountQuery());

    [HttpPost]
    [ProducesResponseType(typeof(AuthenticateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<AuthenticateUserCommandResult> AuthenticateUser([FromBody] AuthenticateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToAuthenticateUserCommand(payLoad));

    [HttpPost]
    [ProducesResponseType(typeof(ReAuthenticateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<ReAuthenticateUserCommandResult> ReAuthenticateUser([FromBody] ReAuthenticateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToReAuthenticateUserCommand(payLoad));

    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RevokeUserRefreshToken([FromBody] RevokeUserRefreshTokenDto payLoad)
        => await Mediator.Send(UsersMapper.MapToRevokeUserRefreshTokenCommand(payLoad));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> ActivateUser([FromBody] ActivateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToActivateUserCommand(payLoad));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> ResetUserPassword([FromBody] ResetUserPasswordDto payLoad) 
        => await Mediator.Send(UsersMapper.MapToResetUserPasswordCommand(payLoad));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserPassword([FromBody] UpdateUserPasswordDto payLoad) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserPasswordCommand(payLoad));

    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IEnumerable<GetAllUsersQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetAllUsersQueryResult>> GetAllUsers([FromQuery] bool noCache = false)
        => await _usersCache.GetUsers(noCache);

    [HttpGet("{id:guid}")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetUserQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserQueryResult> GetUser([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _usersCache.GetUser(id, noCache);

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddUser([FromBody] AddUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToAddUserCommand(payLoad));

    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUser([FromBody] UpdateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToUpdateUserCommand(payLoad));

    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUser([FromBody] RemoveUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToRemoveUserCommand(payLoad));
}