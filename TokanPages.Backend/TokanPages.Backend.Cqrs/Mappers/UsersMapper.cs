﻿namespace TokanPages.Backend.Cqrs.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using Shared.Dto.Users;
    using Handlers.Commands.Users;

    [ExcludeFromCodeCoverage]
    public static class UsersMapper
    {
        public static AuthenticateUserCommand MapToAuthenticateUserCommand(AuthenticateUserDto AModel)
        {
            return new()
            {
                EmailAddress = AModel.EmailAddress,
                Password = AModel.Password
            };
        }

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