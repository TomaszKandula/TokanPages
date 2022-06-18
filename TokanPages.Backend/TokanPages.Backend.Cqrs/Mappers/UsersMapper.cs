namespace TokanPages.Backend.Cqrs.Mappers;

using System.Diagnostics.CodeAnalysis;
using Dto.Users;
using Handlers.Commands.Users;

[ExcludeFromCodeCoverage]
public static class UsersMapper
{
    public static AuthenticateUserCommand MapToAuthenticateUserCommand(AuthenticateUserDto model) => new()
    {
        EmailAddress = model.EmailAddress,
        Password = model.Password
    };

    public static ReAuthenticateUserCommand MapToReAuthenticateUserCommand(ReAuthenticateUserDto model) => new()
    {
        RefreshToken = model.RefreshToken
    };

    public static RevokeUserRefreshTokenCommand MapToRevokeUserRefreshTokenCommand(RevokeUserRefreshTokenDto model) => new()
    {
        RefreshToken = model.RefreshToken
    };

    public static ActivateUserCommand MapToActivateUserCommand(ActivateUserDto model) => new()
    {
        ActivationId = model.ActivationId
    };

    public static ResetUserPasswordCommand MapToResetUserPasswordCommand(ResetUserPasswordDto model) => new()
    {
        EmailAddress = model.EmailAddress
    };

    public static UpdateUserPasswordCommand MapToUpdateUserPasswordCommand(UpdateUserPasswordDto model) => new()
    {
        Id = model.Id,
        ResetId = model.ResetId,
        NewPassword = model.NewPassword
    };

    public static AddUserCommand MapToAddUserCommand(AddUserDto model) => new() 
    { 
        EmailAddress = model.EmailAddress,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Password = model.Password
    };

    public static UpdateUserCommand MapToUpdateUserCommand(UpdateUserDto model) => new()
    {
        Id = model.Id,
        IsActivated = model.IsActivated,
        UserAlias = model.UserAlias,
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress,
        ShortBio = model.ShortBio
    };

    public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto model) => new()
    {
        Id = model.Id
    };
}