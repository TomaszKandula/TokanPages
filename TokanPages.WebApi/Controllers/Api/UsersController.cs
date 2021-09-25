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
    public class UsersController : BaseController
    {
        public UsersController(IMediator AMediator) : base(AMediator) { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<AuthenticateUserCommandResult> AuthenticateUser([FromBody] AuthenticateUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToAuthenticateUserCommand(APayLoad));
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReAuthenticateUserCommandResult> ReAuthenticateUser([FromBody] ReAuthenticateUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToReAuthenticateUserCommand(APayLoad));
        
        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<Unit> RevokeUserRefreshToken([FromBody] RevokeUserRefreshTokenDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToRevokeUserRefreshTokenCommand(APayLoad));
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> ActivateUser([FromBody] ActivateUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToActivateUserCommand(APayLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> ResetUserPassword([FromBody] ResetUserPasswordDto APayLoad) 
            => await FMediator.Send(UsersMapper.MapToResetUserPasswordCommand(APayLoad));
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> UpdateUserPassword([FromBody] UpdateUserPasswordDto APayLoad) 
            => await FMediator.Send(UsersMapper.MapToUpdateUserPasswordCommand(APayLoad));
        
        [HttpGet]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<IEnumerable<GetAllUsersQueryResult>> GetAllUsers()
            => await FMediator.Send(new GetAllUsersQuery());

        [HttpGet("{AId}")]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<GetUserQueryResult> GetUser([FromRoute] Guid AId)
            => await FMediator.Send(new GetUserQuery { Id = AId });

        [HttpPost]
        [AllowAnonymous]
        public async Task<Guid> AddUser([FromBody] AddUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToAddUserCommand(APayLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> UpdateUser([FromBody] UpdateUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToUpdateUserCommand(APayLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> RemoveUser([FromBody] RemoveUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToRemoveUserCommand(APayLoad));
    }
}