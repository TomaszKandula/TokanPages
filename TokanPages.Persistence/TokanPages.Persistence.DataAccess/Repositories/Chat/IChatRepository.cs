using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Persistence.DataAccess.Repositories.Chat.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Chat;

public interface IChatRepository
{
    Task<ChatUserDataDto?> GetChatUserData(Guid userId);

    Task<UserMessage?> GetChatUserMessageData(string chatKey, bool isArchived);

    Task<string?[]> RetrieveChatCache(string[] chatKey);

    Task<bool> CreateChatUserData(string chatKey, string chatData, bool isArchived, DateTime createdAt, Guid createdBy);

    Task<bool> UpdateChatUserMessageData(string chatKey, string chatData, bool isArchived, DateTime modifiedAt, Guid modifiedBy);

    Task<bool> RemoveChatUserCacheById(Guid chatId);

    Task<bool> RemoveChatUserCacheByKey(string chatKey);
}