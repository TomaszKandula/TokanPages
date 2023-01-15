using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using MediatR;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.WebApi.Controllers.Mappers;
using TokanPages.WebApi.Dto.Users;

namespace TokanPages.WebApi.Controllers.Api;

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
    /// <param name="payLoad">User data.</param>
    /// <returns>Object.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(AuthenticateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<AuthenticateUserCommandResult> AuthenticateUser([FromBody] AuthenticateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToAuthenticateUserCommand(payLoad));

    /// <summary>
    /// Re-authenticates user.
    /// </summary>
    /// <param name="payLoad">User data.</param>
    /// <returns>Object.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(ReAuthenticateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<ReAuthenticateUserCommandResult> ReAuthenticateUser([FromBody] ReAuthenticateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToReAuthenticateUserCommand(payLoad));

    /// <summary>
    /// Revokes existing user refresh token.
    /// </summary>
    /// <param name="payLoad">Refresh Token.</param>
    /// <returns>Object.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RevokeUserRefreshToken([FromBody] RevokeUserRefreshTokenDto payLoad)
        => await Mediator.Send(UsersMapper.MapToRevokeUserRefreshTokenCommand(payLoad));

    /// <summary>
    /// Activates existing user account.
    /// </summary>
    /// <param name="payLoad">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> ActivateUser([FromBody] ActivateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToActivateUserCommand(payLoad));

    /// <summary>
    /// Resets existing user password.
    /// </summary>
    /// <param name="payLoad">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> ResetUserPassword([FromBody] ResetUserPasswordDto payLoad) 
        => await Mediator.Send(UsersMapper.MapToResetUserPasswordCommand(payLoad));

    /// <summary>
    /// Updates existing user password.
    /// </summary>
    /// <param name="payLoad">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserPassword([FromBody] UpdateUserPasswordDto payLoad) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserPasswordCommand(payLoad));

    /// <summary>
    /// Returns all registered users.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IEnumerable<GetAllUsersQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetAllUsersQueryResult>> GetAllUsers([FromQuery] bool noCache = false)
        => await _usersCache.GetUsers(noCache);

    /// <summary>
    /// Returns registered user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetUserQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserQueryResult> GetUser([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _usersCache.GetUser(id, noCache);

    /// <summary>
    /// Adds new user account.
    /// </summary>
    /// <param name="payLoad">User data.</param>
    /// <returns>Guid.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddUser([FromBody] AddUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToAddUserCommand(payLoad));

    /// <summary>
    /// Updates existing user account.
    /// </summary>
    /// <param name="payLoad">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUser([FromBody] UpdateUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToUpdateUserCommand(payLoad));

    /// <summary>
    /// Removes existing user account.
    /// </summary>
    /// <param name="payLoad">User data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUser([FromBody] RemoveUserDto payLoad)
        => await Mediator.Send(UsersMapper.MapToRemoveUserCommand(payLoad));

    /// <summary>
    /// Returns user media file by its name.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="blobName">Full blob name (case sensitive).</param>
    /// <returns>File.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [ETagFilter]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "id", "blobName" })]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserMedia([FromRoute] Guid id, [FromQuery] string blobName)
        => await Mediator.Send(new GetUserMediaQuery { Id = id, BlobName = blobName });

    /// <summary>
    /// Allows to upload media file (image/video).
    /// </summary>
    /// <param name="payload">File data.</param>
    /// <param name="skipDb">
    /// Allow to skip database update.
    /// No additional information will be logged.
    /// </param>
    /// <returns>Object with media name.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(UploadUserMediaCommandResult), StatusCodes.Status200OK)]
    public async Task<UploadUserMediaCommandResult> UploadUserMedia([FromForm] UploadUserMediaDto payload, [FromQuery] bool skipDb = false)
        => await Mediator.Send(UsersMapper.MapToUploadUserMediaCommand(payload, skipDb));

    /// <summary>
    /// Removes uploaded user media file (image/video).
    /// </summary>
    /// <param name="payload">Unique full blob name.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUserMedia([FromForm] RemoveUserMediaDto payload)
        => await Mediator.Send(UsersMapper.MapToRemoveUserMediaCommand(payload));
}