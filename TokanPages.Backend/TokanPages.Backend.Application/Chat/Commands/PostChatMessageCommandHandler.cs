using TokanPages.Backend.Application.Chat.Models;
using TokanPages.Backend.Application.Notifications.Web.Command;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebSocketService.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Backend.Application.Chat.Commands;

public class PostChatMessageCommandHandler : RequestHandler<PostChatMessageCommand, PostChatMessageCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IUserService _userService;

    private readonly INotificationService _notificationService;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    public PostChatMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IJsonSerializer jsonSerializer, IUserService userService, 
        INotificationService notificationService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _jsonSerializer = jsonSerializer;
        _userService = userService;
        _notificationService = notificationService;
    }

    public override async Task<PostChatMessageCommandResult> Handle(PostChatMessageCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var userMessage = await DatabaseContext.UserMessages
            .Where(message => message.ChatKey == request.ChatKey)
            .Where(message => !message.IsArchived)
            .SingleOrDefaultAsync(cancellationToken);

        var chatDateTime = _dateTimeService.Now;
        var chatItemId = Guid.NewGuid();

        if (userMessage is not null)
        {
            var data = _jsonSerializer.Deserialize<List<AddChatItem>>(userMessage.ChatData);
            data.Add(new AddChatItem
            {
                Id = chatItemId,
                UserId = userId,
                DateTime = chatDateTime,
                Text = request.Message,
                ChatKey = request.ChatKey
            });

            userMessage.ModifiedAt = chatDateTime;
            userMessage.ModifiedBy = userId;
            userMessage.ChatData = _jsonSerializer.Serialize(data, Formatting.None, Settings);
        }
        else
        {
            var items = new List<AddChatItem>
            {
                new()
                {
                    Id = chatItemId,
                    UserId = userId,
                    DateTime = chatDateTime,
                    Text = request.Message,
                    ChatKey = request.ChatKey
                }
            };

            var newChat = new UserMessage
            {
                CreatedAt = chatDateTime,
                CreatedBy = userId,
                ChatKey = request.ChatKey,
                ChatData = _jsonSerializer.Serialize(items, Formatting.None, Settings)
            };

            await DatabaseContext.UserMessages.AddAsync(newChat, cancellationToken);
        }

        var initials = await GetUserInitials(userId, cancellationToken);
        var avatarName = await GetUserAvatarName(userId, cancellationToken);
        var chatData = new NotifyChatItem
        {
            Id = chatItemId,
            UserId = userId,
            Initials = initials,
            AvatarName = avatarName,
            DateTime = chatDateTime,
            Text = request.Message,
            ChatKey = request.ChatKey
        };

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        await Notify(chatData, cancellationToken);

        return new PostChatMessageCommandResult
        {
            Id = chatItemId,
            DateTime = chatDateTime
        };
    }

    private async Task Notify(NotifyChatItem notification, CancellationToken cancellationToken)
    {
        var notify = new NotifyRequestCommand
        {
            UserId = notification.UserId,
            ExternalPayload = _jsonSerializer.Serialize(notification),
            Handler = "chat_post_message",
            CanSkipPreservation = true
        };

        var handler = new NotifyRequestCommandHandler(DatabaseContext, LoggerService, _notificationService, _jsonSerializer);
        await handler.Handle(notify, cancellationToken);
    }

    private async Task<string> GetUserInitials(Guid userId, CancellationToken cancellationToken)
    {
        var initials = "A";
        var userInfo = await DatabaseContext.UserInformation
            .AsNoTracking()
            .Where(info => info.UserId == userId)
            .Select(info => new
            {
                info.FirstName,
                info.LastName
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
        var blobName = await DatabaseContext.UserInformation
            .AsNoTracking()
            .Where(info => info.UserId == userId)
            .Select(info => info.UserImageName)
            .SingleOrDefaultAsync(cancellationToken);

        return blobName ?? string.Empty;
    }
}