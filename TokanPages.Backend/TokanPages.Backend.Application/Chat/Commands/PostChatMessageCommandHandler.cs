using TokanPages.Backend.Application.Chat.Models;
using TokanPages.Backend.Application.Notifications.Web.Command;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebSocketService.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Chat;

namespace TokanPages.Backend.Application.Chat.Commands;

public class PostChatMessageCommandHandler : RequestHandler<PostChatMessageCommand, PostChatMessageCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IUserService _userService;

    private readonly INotificationService _notificationService;

    private readonly IChatRepository _chatRepository;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    public PostChatMessageCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IJsonSerializer jsonSerializer, IUserService userService, 
        INotificationService notificationService, IChatRepository chatRepository) : base(operationDbContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _jsonSerializer = jsonSerializer;
        _userService = userService;
        _notificationService = notificationService;
        _chatRepository = chatRepository;
    }

    //TODO: change implementation for multiuser chat
    public override async Task<PostChatMessageCommandResult> Handle(PostChatMessageCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var chatDateTime = _dateTimeService.Now;
        var chatItemId = Guid.NewGuid();

        var userMessage = await _chatRepository.GetChatUserMessageData(request.ChatKey, false);
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

            var chatData = _jsonSerializer.Serialize(data, Formatting.None, Settings);
            await _chatRepository.UpdateChatUserMessageData(request.ChatKey, chatData, false, chatDateTime, userId);
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

            var chatData = _jsonSerializer.Serialize(items, Formatting.None, Settings);
            await _chatRepository.CreateChatUserData(request.ChatKey, chatData, false, chatDateTime, userId);
        }

        var user = await _chatRepository.GetChatUserData(userId);
        if (user is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var initials = GetUserInitials(user.FirstName, user.LastName);
        var notification = new NotifyChatItem
        {
            Id = chatItemId,
            UserId = userId,
            Initials = initials,
            AvatarName = user.UserImageName,
            DateTime = chatDateTime,
            Text = request.Message,
            ChatKey = request.ChatKey
        };

        //await Notify(notification, cancellationToken);

        return new PostChatMessageCommandResult
        {
            Id = chatItemId,
            DateTime = chatDateTime
        };
    }

    // TODO: reimplement for repository pattern
    // private async Task Notify(NotifyChatItem notification, CancellationToken cancellationToken)
    // {
    //     var notify = new NotifyRequestCommand
    //     {
    //         UserId = notification.UserId,
    //         ExternalPayload = _jsonSerializer.Serialize(notification),
    //         Handler = "chat_post_message",
    //         CanSkipPreservation = true
    //     };
    //
    //     var handler = new NotifyRequestCommandHandler(OperationDbContext, LoggerService, _notificationService, _jsonSerializer);
    //     await handler.Handle(notify, cancellationToken);
    // }

    private static string GetUserInitials(string? firstName, string? lastName)
    {
        const string initials = "A";
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            return initials;

        return (firstName[..1] + lastName[..1]).ToUpper();
    }
}