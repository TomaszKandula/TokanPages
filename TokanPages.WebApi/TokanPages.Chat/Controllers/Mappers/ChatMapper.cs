using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Chat.Dto.Chat;

namespace TokanPages.Chat.Controllers.Mappers;

/// <summary>
/// Chat mapper.
/// </summary>
public static class ChatMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static PostChatMessageCommand MapToPostNotificationCommand(PostChatMessageDto model) => new()
    {
        ChatKey = model.ChatKey,
        Message = model.Message
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static RetrieveChatCacheCommand MapToRetrieveChatCacheCommand(RetrieveChatCacheDto model) => new()
    {
        ChatKey = model.ChatKey
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static RemoveChatCacheCommand MapToRemoveChatCacheCommand(RemoveChatCacheDto model) => new()
    {
        ChatId = model.ChatId,
        ChatKey = model.ChatKey,
    };
}