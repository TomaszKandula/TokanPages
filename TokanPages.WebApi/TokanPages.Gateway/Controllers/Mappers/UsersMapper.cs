using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Gateway.Dto.Users;

namespace TokanPages.Gateway.Controllers.Mappers;

/// <summary>
/// Users mapper
/// </summary>
[ExcludeFromCodeCoverage]
public static class UsersMapper
{
    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static AuthenticateUserCommand MapToAuthenticateUserCommand(AuthenticateUserDto model) => new()
    {
        EmailAddress = model.EmailAddress,
        Password = model.Password
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static ReAuthenticateUserCommand MapToReAuthenticateUserCommand(ReAuthenticateUserDto model) => new()
    {
        UserId = model.UserId,
        RefreshToken = model.RefreshToken
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static RevokeUserRefreshTokenCommand MapToRevokeUserRefreshTokenCommand(RevokeUserRefreshTokenDto model) => new()
    {
        RefreshToken = model.RefreshToken
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static ActivateUserCommand MapToActivateUserCommand(ActivateUserDto model) => new()
    {
        ActivationId = model.ActivationId
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static ResetUserPasswordCommand MapToResetUserPasswordCommand(ResetUserPasswordDto model) => new()
    {
        EmailAddress = model.EmailAddress
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateUserPasswordCommand MapToUpdateUserPasswordCommand(UpdateUserPasswordDto model) => new()
    {
        Id = model.Id,
        ResetId = model.ResetId,
        NewPassword = model.NewPassword
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static AddUserCommand MapToAddUserCommand(AddUserDto model) => new() 
    { 
        EmailAddress = model.EmailAddress,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Password = model.Password
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
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
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto model) => new()
    {
        Id = model.Id
    };

    /// <summary>
    /// Maps request DTO to a given command.
    /// </summary>
    /// <param name="model">Users object.</param>
    /// <param name="skipDb">
    /// Allow to skip database update.
    /// No additional information will be logged.
    /// </param>
    /// <returns>Command object.</returns>
    public static UploadImageCommand MapToUploadImageCommand(UploadImageDto model, bool skipDb = false) => new()
    {
        SkipDb = skipDb,
        Base64Data = model.Base64Data,
        BinaryData = model.BinaryData
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static RemoveUserMediaCommand MapToRemoveUserMediaCommand(RemoveUserMediaDto model) => new()
    {
        UniqueBlobName = model.UniqueBlobName
    };
}