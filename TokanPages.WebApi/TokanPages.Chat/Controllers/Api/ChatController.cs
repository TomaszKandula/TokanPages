using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Application.Chat.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Chat.Dto.Chat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Chat.Controllers.Api;

/// <summary>
/// API endpoints definitions for application chat.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class ChatController : ApiBaseController
{
    /// <inheritdoc />
    public ChatController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Post new chat message.
    /// </summary>
    /// <param name="payload">Contains of Sender ID, Receiver ID and message.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(PostChatMessageCommandResult), StatusCodes.Status200OK)]
    public async Task<PostChatMessageCommandResult> PostChatMessage([FromBody] PostChatMessageDto payload) 
        => await Mediator.Send(Mappers.ChatMapper.MapToPostNotificationCommand(payload));

    /// <summary>
    /// Returns current chat for given chat key.
    /// </summary>
    /// <param name="chatKey">Mandatory chat key.</param>
    /// <remarks>
    /// Requires: Roles.OrdinaryUser.
    /// </remarks>
    /// <returns>Chat data.</returns>
    [HttpGet]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(GetChatDataQueryResult), StatusCodes.Status200OK)]
    public async Task<GetChatDataQueryResult> GetChatData([FromQuery] string chatKey) 
        => await Mediator.Send(new GetChatDataQuery { ChatKey = chatKey});

    /// <summary>
    /// Returns cached user messages for given chat keys.
    /// </summary>
    /// <param name="payload">List of chat keys.</param>
    /// <remarks>
    /// Requires: Roles.OrdinaryUser.
    /// </remarks>
    /// <returns>Cached user messages.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(RetrieveChatCacheCommandResult), StatusCodes.Status200OK)]
    public async Task<RetrieveChatCacheCommandResult> RetrieveChatCache([FromBody] RetrieveChatCacheDto payload) 
        => await Mediator.Send(Mappers.ChatMapper.MapToRetrieveChatCacheCommand(payload));

    /// <summary>
    /// Removes cached user messages for given chat key.
    /// </summary>
    /// <param name="payload">Chat key.</param>
    /// <remarks>
    /// Requires: Roles.OrdinaryUser.
    /// </remarks>
    /// <returns>Empty object.</returns>
    [HttpDelete]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveChatCache([FromBody] RemoveChatCacheDto payload) 
        => await Mediator.Send(Mappers.ChatMapper.MapToRemoveChatCacheCommand(payload));
}