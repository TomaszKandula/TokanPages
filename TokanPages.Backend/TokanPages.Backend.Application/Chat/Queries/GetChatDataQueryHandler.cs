using TokanPages.Backend.Application.Chat.Models;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TokanPages.Backend.Application.Chat.Queries;

public class GetChatDataQueryHandler : RequestHandler<GetChatDataQuery, GetChatDataQueryResult>
{
    private readonly IJsonSerializer _jsonSerializer;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    public GetChatDataQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IJsonSerializer jsonSerializer) : base(databaseContext, loggerService) => _jsonSerializer = jsonSerializer;

    public override async Task<GetChatDataQueryResult> Handle(GetChatDataQuery request, CancellationToken cancellationToken)
    {
        var chatData = await DatabaseContext.UserMessages
            .AsNoTracking()
            .Where(messages => messages.ChatKey == request.ChatKey)
            .Where(messages => !messages.IsArchived)
            .Select(messages => messages.ChatData)
            .SingleOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(chatData))
            return new GetChatDataQueryResult { ChatData = string.Empty };

        var decodedKey = request.ChatKey.ToBase64Decode();
        var arrayKey = decodedKey.Split(':');
        var id1 = Guid.Parse(arrayKey[0]);
        var id2 = Guid.Parse(arrayKey[1]);

        var initialsUser1 = await GetUserInitials(id1, cancellationToken);
        var avatarUser1 = await GetUserAvatarName(id1, cancellationToken);

        var initialsUser2 = await GetUserInitials(id2, cancellationToken);
        var avatarUser2 = await GetUserAvatarName(id2, cancellationToken);

        var data = _jsonSerializer.Deserialize<List<GetChatItem>>(chatData);
        foreach (var item in data)
        {
            if (item.UserId == id1)
            {
                item.AvatarName = avatarUser1;
                item.Initials = initialsUser1;
            }

            if (item.UserId == id2)
            {
                item.AvatarName = avatarUser2;
                item.Initials = initialsUser2;
            }
        }

        var enriched = _jsonSerializer.Serialize(data, Formatting.None, Settings);
        return new GetChatDataQueryResult { ChatData = enriched };
    }

    private async Task<string> GetUserInitials(Guid userId, CancellationToken cancellationToken)
    {
        var initials = "A";
        var userInfo = await DatabaseContext.UserInfo
            .AsNoTracking()
            .Where(users => users.Id == userId)
            .Select(users => new
            {
                users.FirstName,
                users.LastName
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (userInfo is null)
            return initials;

        if (userInfo is not { FirstName: "", LastName: "" })
            initials = (userInfo.FirstName[..1] + userInfo.LastName[..1]).ToUpper();

        return initials;
    }

    private async Task<string> GetUserAvatarName(Guid userId, CancellationToken cancellationToken)
    {
        var blobName = await DatabaseContext.UserInfo
            .AsNoTracking()
            .Where(users => users.UserId == userId)
            .Select(users => users.UserImageName)
            .SingleOrDefaultAsync(cancellationToken);

        return blobName ?? string.Empty;
    }
}
