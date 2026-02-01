using TokanPages.Backend.Application.Chat.Models;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Chat;

namespace TokanPages.Backend.Application.Chat.Queries;

public class GetChatDataQueryHandler : RequestHandler<GetChatDataQuery, GetChatDataQueryResult>
{
    private readonly IJsonSerializer _jsonSerializer;

    private readonly IChatRepository _chatRepository;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    public GetChatDataQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IJsonSerializer jsonSerializer, IChatRepository chatRepository) : base(operationDbContext, loggerService)
    {
        _jsonSerializer = jsonSerializer;
        _chatRepository = chatRepository;
    }

    public override async Task<GetChatDataQueryResult> Handle(GetChatDataQuery request, CancellationToken cancellationToken)
    {
        var userMessage = await _chatRepository.GetChatUserMessageData(request.ChatKey, false);
        if (userMessage is null)
            return new GetChatDataQueryResult { ChatData = string.Empty };

        var chatData = userMessage.ChatData;        
        if (string.IsNullOrWhiteSpace(chatData))
            return new GetChatDataQueryResult { ChatData = string.Empty };

        var decodedKey = request.ChatKey.ToBase64Decode();
        var arrayKey = decodedKey.Split(':');

        //TODO: reimplement this to allow many IDs
        var id1 = Guid.Parse(arrayKey[0]);
        var id2 = Guid.Parse(arrayKey[1]);

        var user1data = await _chatRepository.GetChatUserData(id1);
        var user2data = await _chatRepository.GetChatUserData(id2);

        var data = _jsonSerializer.Deserialize<List<GetChatItem>>(chatData);
        foreach (var item in data)
        {
            if (user1data is not null && item.UserId == id1)
            {
                item.AvatarName = user1data.UserImageName ?? string.Empty;
                item.Initials = GetUserInitials(user1data.FirstName, user1data.LastName);
            }

            if (user2data is not null && item.UserId == id2)
            {
                item.AvatarName = user2data.UserImageName;
                item.Initials = GetUserInitials(user2data.FirstName, user2data.LastName);
            }
        }

        var enriched = _jsonSerializer.Serialize(data, Formatting.None, Settings);
        return new GetChatDataQueryResult { ChatData = enriched };
    }

    private static string GetUserInitials(string? firstName, string? lastName)
    {
        const string initials = "A";
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            return initials;

        return (firstName[..1] + lastName[..1]).ToUpper();
    }
}
