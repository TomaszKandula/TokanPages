using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Extensions;
using TokanPages.WebApi.Dto.Users;

namespace TokanPages.WebApi.Controllers.Mappers;

/// <summary>
/// Users mapper
/// </summary>
[ExcludeFromCodeCoverage]
public static class UsersMapper
{
    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">AuthenticateUserDto</param>
    /// <returns>AuthenticateUserCommand</returns>
    public static AuthenticateUserCommand MapToAuthenticateUserCommand(AuthenticateUserDto model) => new()
    {
        EmailAddress = model.EmailAddress,
        Password = model.Password
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">ReAuthenticateUserDto</param>
    /// <returns>ReAuthenticateUserCommand</returns>
    public static ReAuthenticateUserCommand MapToReAuthenticateUserCommand(ReAuthenticateUserDto model) => new()
    {
        RefreshToken = model.RefreshToken
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">RevokeUserRefreshTokenDto</param>
    /// <returns>RevokeUserRefreshTokenCommand</returns>
    public static RevokeUserRefreshTokenCommand MapToRevokeUserRefreshTokenCommand(RevokeUserRefreshTokenDto model) => new()
    {
        RefreshToken = model.RefreshToken
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">ActivateUserDto</param>
    /// <returns>ActivateUserCommand</returns>
    public static ActivateUserCommand MapToActivateUserCommand(ActivateUserDto model) => new()
    {
        ActivationId = model.ActivationId
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">ResetUserPasswordDto</param>
    /// <returns>ResetUserPasswordCommand</returns>
    public static ResetUserPasswordCommand MapToResetUserPasswordCommand(ResetUserPasswordDto model) => new()
    {
        EmailAddress = model.EmailAddress
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">UpdateUserPasswordDto</param>
    /// <returns>UpdateUserPasswordCommand</returns>
    public static UpdateUserPasswordCommand MapToUpdateUserPasswordCommand(UpdateUserPasswordDto model) => new()
    {
        Id = model.Id,
        ResetId = model.ResetId,
        NewPassword = model.NewPassword
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">AddUserDto</param>
    /// <returns>AddUserCommand</returns>
    public static AddUserCommand MapToAddUserCommand(AddUserDto model) => new() 
    { 
        EmailAddress = model.EmailAddress,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Password = model.Password
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">UpdateUserDto</param>
    /// <returns>UpdateUserCommand</returns>
    public static UpdateUserCommand MapToUpdateUserCommand(UpdateUserDto model) => new()
    {
        Id = model.Id,
        IsActivated = model.IsActivated,
        UserAlias = model.UserAlias,
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress,
        UserAboutText = model.UserAboutText,
        UserImageName = model.UserImageName,
        UserVideoName = model.UserVideoName
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">RemoveUserDto</param>
    /// <returns>RemoveUserCommand</returns>
    public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto model) => new()
    {
        Id = model.Id
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">UploadUserMediaDto</param>
    /// <returns>UploadUserMediaCommand</returns>
    public static UploadUserMediaCommand MapToUploadUserMediaCommand(UploadUserMediaDto model) => new()
    {
        UserId = model.UserId,
        MediaTarget = model.MediaTarget,
        MediaName = model.Data!.FileName,
        MediaType = model.Data.ContentType.ToMediaType(),
        Data = model.Data.GetByteArray()
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">RemoveUserMediaDto</param>
    /// <returns>RemoveUserMediaCommand</returns>
    public static RemoveUserMediaCommand MapToRemoveUserMediaCommand(RemoveUserMediaDto model) => new()
    {
        UniqueBlobName = model.UniqueBlobName
    };
}