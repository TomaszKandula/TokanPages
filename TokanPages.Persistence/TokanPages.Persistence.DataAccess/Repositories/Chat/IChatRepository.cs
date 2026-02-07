using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Persistence.DataAccess.Repositories.Chat.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Chat;

public interface IChatRepository
{
    Task<ChatUserDataDto?> GetChatUserData(Guid userId);

    Task<UserMessage?> GetChatUserMessageData(string chatKey, bool isArchived);

    Task<string[]> RetrieveChatCache(string[] chatKey);

    Task CreateChatUserData(string chatKey, string chatData, bool isArchived, DateTime createdAt, Guid createdBy);

    Task UpdateChatUserMessageData(string chatKey, string chatData, bool isArchived, DateTime modifiedAt, Guid modifiedBy);

    Task CreateChatCache(Guid id, string chatKey, string notification);
    
    Task RemoveChatUserCacheById(Guid chatId);

    Task RemoveChatUserCacheByKey(string chatKey);
}