using MediatR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;

namespace TokanPages.Controllers.Api
{
    public class UsersController : BaseController
    {
        public UsersController(IMediator AMediator) : base(AMediator) { }

        [HttpGet]
        public async Task<IEnumerable<GetAllUsersQueryResult>> GetAllUsers()
            => await FMediator.Send(new GetAllUsersQuery());

        [HttpGet("{AId}")]
        public async Task<GetUserQueryResult> GetUser([FromRoute] Guid AId)
            => await FMediator.Send(new GetUserQuery { Id = AId });

        [HttpPost]
        public async Task<Guid> AddUser([FromBody] AddUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToAddUserCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> UpdateUser([FromBody] UpdateUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToUpdateUserCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> RemoveUser([FromBody] RemoveUserDto APayLoad)
            => await FMediator.Send(UsersMapper.MapToRemoveUserCommand(APayLoad));
    }
}
