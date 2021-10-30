namespace TokanPages.WebApi.Controllers.Api
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Mappers;
    using Backend.Shared.Dto.Users;
    using Backend.Identity.Attributes;
    using Backend.Identity.Authorization;
    using Backend.Cqrs.Handlers.Queries.Users;
    using Backend.Cqrs.Handlers.Commands.Users;
    using MediatR;

    [Authorize]
    public class UsersController : ApiBaseController
    {
        public UsersController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<AuthenticateUserCommandResult> AuthenticateUser([FromBody] AuthenticateUserDto payLoad)
            => await Mediator.Send(UsersMapper.MapToAuthenticateUserCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<ReAuthenticateUserCommandResult> ReAuthenticateUser([FromBody] ReAuthenticateUserDto payLoad)
            => await Mediator.Send(UsersMapper.MapToReAuthenticateUserCommand(payLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<Unit> RevokeUserRefreshToken([FromBody] RevokeUserRefreshTokenDto payLoad)
            => await Mediator.Send(UsersMapper.MapToRevokeUserRefreshTokenCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> ActivateUser([FromBody] ActivateUserDto payLoad)
            => await Mediator.Send(UsersMapper.MapToActivateUserCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> ResetUserPassword([FromBody] ResetUserPasswordDto payLoad) 
            => await Mediator.Send(UsersMapper.MapToResetUserPasswordCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> UpdateUserPassword([FromBody] UpdateUserPasswordDto payLoad) 
            => await Mediator.Send(UsersMapper.MapToUpdateUserPasswordCommand(payLoad));

        [HttpGet]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<IEnumerable<GetAllUsersQueryResult>> GetAllUsers()
            => await Mediator.Send(new GetAllUsersQuery());

        [HttpGet("{id:guid}")]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<GetUserQueryResult> GetUser([FromRoute] Guid id)
            => await Mediator.Send(new GetUserQuery { Id = id });

        [HttpPost]
        [AllowAnonymous]
        public async Task<Guid> AddUser([FromBody] AddUserDto payLoad)
            => await Mediator.Send(UsersMapper.MapToAddUserCommand(payLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> UpdateUser([FromBody] UpdateUserDto payLoad)
            => await Mediator.Send(UsersMapper.MapToUpdateUserCommand(payLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> RemoveUser([FromBody] RemoveUserDto payLoad)
            => await Mediator.Send(UsersMapper.MapToRemoveUserCommand(payLoad));
    }
}