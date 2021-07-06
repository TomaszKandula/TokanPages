using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Identity.Attributes;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;
using MediatR;

namespace TokanPages.WebApi.Controllers.Api
{
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
