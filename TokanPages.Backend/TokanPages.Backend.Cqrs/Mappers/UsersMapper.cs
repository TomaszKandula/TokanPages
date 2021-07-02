using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace TokanPages.Backend.Cqrs.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class UsersMapper
    {
        public static AddUserCommand MapToAddUserCommand(AddUserDto AModel) 
        {
            return new () 
            { 
                EmailAddress = AModel.EmailAddress,
                UserAlias = AModel.UserAlias,
                FirstName = AModel.FirstName,
                LastName = AModel.LastName,
                Password = AModel.Password
            };
        }

        public static UpdateUserCommand MapToUpdateUserCommand(UpdateUserDto AModel)
        {
            return new ()
            {
                Id = AModel.Id,
                UserAlias = AModel.UserAlias,
                IsActivated = AModel.IsActivated,
                FirstName = AModel.FirstName,
                LastName = AModel.LastName,
                EmailAddress = AModel.EmailAddress,
            };
        }

        public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto AModel)
        {
            return new ()
            {
                Id = AModel.Id
            };
        }
    }
}
