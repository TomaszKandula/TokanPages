using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Options;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Chat.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Chat;

public class ChatRepository : RepositoryBase, IChatRepository
{
    public ChatRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations, appSettings) { }

    public async Task<ChatUserDataDto?> GetChatUserData(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = await DbOperations.Retrieve<UserInfo>(filterBy);

        var result = data.SingleOrDefault();
        if (result is null)
            return null;

        return new ChatUserDataDto
        {
            FirstName = result.FirstName,
            LastName = result.LastName,
            UserImageName = result.UserImageName ?? string.Empty,
        };
    }

    public async Task CreateChatUserData(string chatKey, string chatData, bool isArchived, DateTime createdAt, Guid createdBy)
    {
        var entity = new UserMessage
        {
            Id = Guid.NewGuid(),
            ChatKey = chatKey,
            ChatData = chatData,
            IsArchived = isArchived,
            CreatedAt = createdAt,
            CreatedBy = createdBy,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await DbOperations.Insert(entity);
    }

    public async Task<UserMessage?> GetChatUserMessageData(string chatKey, bool isArchived)
    {
        var filterBy = new { ChatKey = chatKey, IsArchived = isArchived };
        var result =  await DbOperations.Retrieve<UserMessage>(filterBy);
        return result.SingleOrDefault();
    }

    public async Task UpdateChatUserMessageData(string chatKey, string chatData, bool isArchived, DateTime modifiedAt, Guid modifiedBy)
    {
        var updateBy = new
        {
            ChatData = chatData,
            ModifiedAt = modifiedAt,
            ModifiedBy = modifiedBy
        };

        var filterBy = new
        {
            ChatKey = chatKey,
            IsArchived = isArchived
        };

        await DbOperations.Update<UserMessage>(updateBy, filterBy);
    }

    public async Task<string[]> GetChatCache(string[] chatKey)
    {
        const string query = @"
            SELECT
                operation.Notification
            FROM
                operation.UserMessagesCache
            WHERE
                operation.ChatKey IN @ChatKey
        ";

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var result = await connection.QueryAsync<string>(query, new { ChatKey = chatKey });
        return result.ToArray();
    }

    public async Task CreateChatCache(Guid id, string chatKey, string notification)
    {
        var entity = new UserMessageCache
        {
            Id = id,
            ChatKey = chatKey,
            Notification = notification
        };

        await DbOperations.Insert(entity);
    }

    public async Task RemoveChatUserCacheById(Guid chatId)
    {
        var filterBy = new { ChatId = chatId };
        await DbOperations.Delete<UserMessageCache>(filterBy);
    }

    public async Task RemoveChatUserCacheByKey(string chatKey)
    {
        var filterBy = new { ChatKey = chatKey };
        await DbOperations.Delete<UserMessageCache>(filterBy);
    }
}