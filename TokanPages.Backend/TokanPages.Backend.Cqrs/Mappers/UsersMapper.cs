﻿namespace TokanPages.Backend.Cqrs.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using Shared.Dto.Users;
    using Handlers.Commands.Users;

    [ExcludeFromCodeCoverage]
    public static class UsersMapper
    {
        public static AuthenticateUserCommand MapToAuthenticateUserCommand(AuthenticateUserDto AModel) => new ()
        {
            EmailAddress = AModel.EmailAddress,
            Password = AModel.Password
        };

        public static ReAuthenticateUserCommand MapToReAuthenticateUserCommand(ReAuthenticateUserDto AModel) => new ()
        {
            Id = AModel.Id
        };

        public static RevokeUserRefreshTokenCommand MapToRevokeUserRefreshTokenCommand(RevokeUserRefreshTokenDto AModel) => new ()
        {
            RefreshToken = AModel.RefreshToken
        };

        public static ActivateUserCommand MapToActivateUserCommand(ActivateUserDto AModel) => new ()
        {
            ActivationId = AModel.ActivationId
        };
        
        public static ResetUserPasswordCommand MapToResetUserPasswordCommand(ResetUserPasswordDto AModel) => new ()
        {
            EmailAddress = AModel.EmailAddress
        };

        public static UpdateUserPasswordCommand MapToUpdateUserPasswordCommand(UpdateUserPasswordDto AModel) => new ()
        {
            Id = AModel.Id,
            ResetId = AModel.ResetId,
            NewPassword = AModel.NewPassword
        };
        
        public static AddUserCommand MapToAddUserCommand(AddUserDto AModel) => new () 
        { 
            EmailAddress = AModel.EmailAddress,
            UserAlias = AModel.UserAlias,
            FirstName = AModel.FirstName,
            LastName = AModel.LastName,
            Password = AModel.Password
        };

        public static UpdateUserCommand MapToUpdateUserCommand(UpdateUserDto AModel) => new ()
        {
            Id = AModel.Id,
            UserAlias = AModel.UserAlias,
            IsActivated = AModel.IsActivated,
            FirstName = AModel.FirstName,
            LastName = AModel.LastName,
            EmailAddress = AModel.EmailAddress,
        };

        public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto AModel) => new ()
        {
            Id = AModel.Id
        };
    }
}