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
    using MediatR;

    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(IMediator AMediator) : base(AMediator) { }

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