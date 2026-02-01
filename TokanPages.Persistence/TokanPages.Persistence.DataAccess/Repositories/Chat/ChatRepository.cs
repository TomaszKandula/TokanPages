using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Chat.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Chat;

public class ChatRepository : IChatRepository
{
    private readonly IDbOperations _dbOperations;

    private readonly AppSettingsModel _appSettings;

    public ChatRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> options)
    {
        _dbOperations = dbOperations;
        _appSettings = options.Value;
    }

    public async Task<ChatUserDataDto?> GetChatUserData(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = (await _dbOperations.Retrieve<UserInfo>(filterBy)).SingleOrDefault();
        if (data == null)
            return null;

        return new ChatUserDataDto
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            UserImageName = data.UserImageName,
        };
    }

    public async Task<UserMessage?> GetChatUserMessageData(string chatKey, bool isArchived)
    {
        var filterBy = new { ChatKey = chatKey, IsArchived = isArchived };
        return (await _dbOperations.Retrieve<UserMessage>(filterBy)).SingleOrDefault();
    }

    public async Task<string?[]> RetrieveChatCache(string[] chatKey)
    {
        const string query = @"
            SELECT
                operation.Notification
            FROM
                operation.UserMessagesCache
            WHERE
                operation.ChatKey IN @ChatKey
        ";

        await using var db = new SqlConnection(_appSettings.DbDatabaseContext);
        return (await db.QueryAsync<string?>(query, new { ChatKey = chatKey })).ToArray();
    }

    public async Task<bool> CreateChatUserData(string chatKey, string chatData, bool isArchived, DateTime createdAt, Guid createdBy)
    {
        try
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

            await _dbOperations.Insert(entity);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateChatUserMessageData(string chatKey, string chatData, bool isArchived, DateTime modifiedAt, Guid modifiedBy)
    {
        try
        {
            var updateBy = new { ChatData = chatData, ModifiedAt = modifiedAt, ModifiedBy = modifiedBy };
            var filterBy = new { ChatKey = chatKey, IsArchived = isArchived };
            await _dbOperations.Update<UserMessage>(updateBy, filterBy);
        }
        catch
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> RemoveChatUserCacheById(Guid chatId)
    {
        try
        {
            var filterBy = new { ChatId = chatId };
            await _dbOperations.Delete<UserMessageCache>(filterBy);
        }
        catch
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> RemoveChatUserCacheByKey(string chatKey)
    {
        try
        {
            var filterBy = new { ChatKey = chatKey };
            await _dbOperations.Delete<UserMessageCache>(filterBy);
        }
        catch
        {
            return false;
        }
        
        return true;
    }
}