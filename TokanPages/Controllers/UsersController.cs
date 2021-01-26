using MediatR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;

namespace TokanPages.Controllers
{
    public class UsersController : __BaseController
    {
        public UsersController(IMediator AMediator) : base(AMediator)
        { 
        }

        [HttpGet]
        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var LQuery = new GetAllUsersQuery();
            return await FMediator.Send(LQuery);
        }

        [HttpGet("{Id}")]
        public async Task<Users> GetUser([FromRoute] Guid Id)
        {
            var LQuery = new GetUserQuery { Id = Id };
            return await FMediator.Send(LQuery);
        }

        [HttpPost]
        public async Task<Guid> AddUser([FromBody] AddUserDto APayLoad)
        {
            var LCommand = UsersMapper.MapToAddUserCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> UpdateUser([FromBody] UpdateUserDto APayLoad)
        {
            var LCommand = UsersMapper.MapToUpdateUserCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> RemoveUser([FromBody] RemoveUserDto APayLoad)
        {
            var LCommand = UsersMapper.MapToRemoveUserCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }
    }
}
