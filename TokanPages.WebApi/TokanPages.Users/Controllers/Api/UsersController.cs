using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using MediatR;
using Microsoft.AspNetCore.RateLimiting;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Users.Controllers.Mappers;
using TokanPages.Users.Dto.Users;

namespace TokanPages.Users.Controllers.Api;

/// <summary>
/// API endpoints definitions for users.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ApiBaseController
{
    private readonly IUsersCache _usersCache;

    /// <inheritdoc />
    public UsersController(IMediator mediator, IUsersCache usersCache) 
        : base(mediator) => _usersCache = usersCache;

    /// <summary>
    /// Returns visitor count.
    /// </summary>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(typeof(GetUserVisitCountQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserVisitCountQueryResult> GetVisitCount()
        => await Mediator.Send(new GetUserVisitCountQuery());

    /// <summary>
    /// Authenticates user.
    /// </summary>
    /// <param name="payload">User data.</param>
    /// <returns>Object.</returns>
    [HttpPost]
    [Route("[action]")]
    [EnableRateLimiting("SigninRateLimiter")]
    [ProducesResponseType(typeof(AuthenticateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<AuthenticateUserCommandResult> AuthenticateUser([FromBody] AuthenticateUserDto payload)
        => await Mediator.Send(UsersMapper.MapToAuthenticateUserCommand(payload));

    /// <summary>
    /// Re-authenticates user.
    /// </summary>
    /// <param name="payload">User data.</param>
    /// <returns>Object.</returns>
    [HttpPost]
    [Route("[action]")]
    [EnableRateLimiting("SigninRateLimiter")]
    [ProducesResponseType(typeof(ReAuthenticateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<ReAuthenticateUserCommandResult> ReAuthenticateUser([FromBody] ReAuthenticateUserDto payload)
        => await Mediator.Send(UsersMapper.MapToReAuthenticateUserCommand(payload));

    /// <summary>
    /// Revokes existing user token.
    /// </summary>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RevokeUserToken()
        => await Mediator.Send(new RevokeUserTokenCommand());

    /// <summary>
    /// Revokes existing user refresh token.
    /// </summary>
    /// <param name="payload">Optional payload.</param>
    /// <returns>Object.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RevokeUserRefreshToken([FromBody] RevokeUserRefreshTokenDto payload)
        => await Mediator.Send(new RevokeUserRefreshTokenCommand());

    /// <summary>
    /// Activates existing user account.
    /// </summary>
    /// <param name="payload">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(ActivateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<ActivateUserCommandResult> ActivateUser([FromBody] ActivateUserDto payload)
        => await Mediator.Send(UsersMapper.MapToActivateUserCommand(payload));

    /// <summary>
    /// Allows to request a user email verification.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.EverydayUser.
    /// </remarks>
    /// <param name="payload">Email address.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RequestEmailVerification([FromBody] VerifyUserEmailDto payload) 
        => await Mediator.Send(UsersMapper.MapToVerifyUserEmailCommand(payload));

    /// <summary>
    /// Resets existing user password.
    /// </summary>
    /// <param name="payload">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> ResetUserPassword([FromBody] ResetUserPasswordDto payload) 
        => await Mediator.Send(UsersMapper.MapToResetUserPasswordCommand(payload));

    /// <summary>
    /// Updates existing user password.
    /// </summary>
    /// <param name="payload">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserPassword([FromBody] UpdateUserPasswordDto payload) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserPasswordCommand(payload));

    /// <summary>
    /// Returns all registered users.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.GodOfAsgard.
    /// </remarks>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("[action]")]
    [AuthorizeUser(Role.GodOfAsgard)]
    [ProducesResponseType(typeof(IEnumerable<GetUsersQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetUsersQueryResult>> GetAllUsers([FromQuery] bool noCache = false)
        => await _usersCache.GetUsers(noCache);

    /// <summary>
    /// Returns registered user.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.GodOfAsgard, Roles.EverydayUser.
    /// </remarks>
    /// <param name="id">User ID.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [AuthorizeUser(Role.GodOfAsgard, Role.EverydayUser)]
    [ProducesResponseType(typeof(GetUserQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserQueryResult> GetUser([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _usersCache.GetUser(id, noCache);

    /// <summary>
    /// Adds new user account.
    /// </summary>
    /// <param name="payload">User data.</param>
    /// <returns>Guid.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddUser([FromBody] AddUserDto payload)
        => await Mediator.Send(UsersMapper.MapToAddUserCommand(payload));

    /// <summary>
    /// Updates existing user account.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.GodOfAsgard, Roles.EverydayUser.
    /// </remarks>
    /// <param name="payload">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Role.GodOfAsgard, Role.EverydayUser)]
    [ProducesResponseType(typeof(UpdateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<UpdateUserCommandResult> UpdateUser([FromBody] UpdateUserDto payload)
        => await Mediator.Send(UsersMapper.MapToUpdateUserCommand(payload));

    /// <summary>
    /// Removes existing user account.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.GodOfAsgard, Roles.EverydayUser.
    /// </remarks>
    /// <param name="payload">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpDelete]
    [Route("[action]")]
    [AuthorizeUser(Role.GodOfAsgard, Role.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUser([FromBody] RemoveUserDto payload)
        => await Mediator.Send(UsersMapper.MapToRemoveUserCommand(payload));

    /// <summary>
    /// Returns user files.
    /// </summary>
    /// <param name="type">Type of files to be returned.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(GetUserFileListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserFileListQueryResult> GetUserFileList([FromQuery] UserFileToReceive type = UserFileToReceive.Any, bool noCache = false) 
        => await _usersCache.GetUserFileList(type, noCache);

    /// <summary>
    /// Adds user file for given type.
    /// </summary>
    /// <param name="payload">File details.</param>
    /// <remarks>
    /// Requires: Roles.EverydayUser.
    /// </remarks>
    /// <returns></returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(AddUserFileCommandResult), StatusCodes.Status200OK)]
    public async Task<AddUserFileCommandResult> AddUserFile([FromForm] AddUserFileDto payload) 
        => await Mediator.Send(UsersMapper.MapToAddUserFileCommand(payload));

    /// <summary>
    /// Removes given user file.
    /// </summary>
    /// <param name="payload">File details.</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUserFile([FromBody] RemoveUserFileDto payload) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserFileCommand(payload));

    /// <summary>
    /// Returns user notes for given user ID (taken from authorization header).
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <remarks>
    /// Requires: Roles.EverydayUser.
    /// </remarks>
    /// <returns>Object with list of notes.</returns>
    [HttpGet]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(GetUserNotesQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserNotesQueryResult> GetUserNotes([FromQuery] bool noCache = false) 
        => await _usersCache.GetUserNotes(noCache);

    /// <summary>
    /// Returns user note for given ID.
    /// </summary>
    /// <param name="id">User Note ID.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <remarks>
    /// Requires: Roles.EverydayUser.
    /// </remarks>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(GetUserNoteQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserNoteQueryResult> GetUserNote([FromRoute] Guid id, [FromQuery] bool noCache = false) 
        => await _usersCache.GetUserNote(id, noCache);

    /// <summary>
    /// Adds new user note.
    /// </summary>
    /// <param name="payload">New note details.</param>
    /// <returns>Object with details.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(AddUserNoteCommandResult), StatusCodes.Status200OK)]
    public async Task<AddUserNoteCommandResult> AddUserNote([FromBody] AddUserNoteDto payload) 
        => await Mediator.Send(UsersMapper.MapToAddUserNoteCommand(payload));

    /// <summary>
    /// Updates existing user note.
    /// </summary>
    /// <param name="payload">New note details.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserNote([FromBody] UpdateUserNoteDto payload) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserNoteCommand(payload));

    /// <summary>
    /// Removes existing user note.
    /// </summary>
    /// <param name="payload">New note details.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpDelete]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUserNote([FromBody] RemoveUserNoteDto payload) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserNoteCommand(payload));

    /// <summary>
    /// Returns user image file by its name.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="blobName">Full blob name (case-sensitive).</param>
    /// <returns>File.</returns>
    [HttpGet]
    [ETagFilter]
    [Route("{id:guid}/[action]")]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "id", "blobName" })]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserImage([FromRoute] Guid id, [FromQuery] string blobName)
        => await Mediator.Send(new GetUserImageQuery { Id = id, BlobName = blobName });

    /// <summary>
    /// Allows to upload image file.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.EverydayUser.
    /// </remarks>
    /// <param name="payload">File data.</param>
    /// <param name="skipDb">
    /// Allow to skip database update.
    /// No additional information will be logged.
    /// </param>
    /// <returns>Object with media name.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(UploadImageCommandResult), StatusCodes.Status200OK)]
    public async Task<UploadImageCommandResult> UploadImage([FromForm] UploadImageDto payload, [FromQuery] bool skipDb = false)
        => await Mediator.Send(UsersMapper.MapToUploadImageCommand(payload, skipDb));

    /// <summary>
    /// Removes uploaded user media file (image/video).
    /// </summary>
    /// <remarks>
    /// Requires: Roles.EverydayUser.
    /// </remarks>
    /// <param name="payload">Unique full blob name.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpDelete]
    [Route("[action]")]
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUserMedia([FromForm] RemoveUserMediaDto payload)
        => await Mediator.Send(UsersMapper.MapToRemoveUserMediaCommand(payload));
}