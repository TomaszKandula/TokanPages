using TokanPages.Backend.Application.Notifications.Web.Models;
using TokanPages.Backend.Application.Notifications.Web.Models.Base;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.WebSocketService.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Chat;
using TokanPages.Persistence.DataAccess.Repositories.Notification;
using TokanPages.Persistence.DataAccess.Repositories.User;

namespace TokanPages.Backend.Application.Notifications.Web.Command;

public class NotifyRequestCommandHandler : RequestHandler<NotifyRequestCommand, NotifyRequestCommandResult>
{
    private readonly INotificationService _notificationService;

    private readonly INotificationRepository _notificationRepository;

    private readonly IChatRepository _chatRepository;
    
    private readonly IUserRepository _userRepository;

    private readonly IJsonSerializer _jsonSerializer;

    private static JsonSerializerSettings Setting => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    public NotifyRequestCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, INotificationService notificationService, 
        IJsonSerializer jsonSerializer, INotificationRepository notificationRepository, IChatRepository chatRepository, IUserRepository userRepository) 
        : base(operationDbContext, loggerService)
    {
        _notificationService = notificationService;
        _jsonSerializer = jsonSerializer;
        _notificationRepository = notificationRepository;
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }

    public override async Task<NotifyRequestCommandResult> Handle(NotifyRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
            throw new GeneralException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        object? payload = null;
        switch (request.Handler)
        {
            case "user_activated":
                payload = new StatusBase
                {
                    UserId = user.Id,
                    Handler = request.Handler,
                    Payload = new UserActivationData
                    {
                        IsActivated = user.IsActivated,
                        IsVerified = user.IsVerified,
                        HasBusinessLock = user.HasBusinessLock
                    }
                };
                break;
            case "payment_status":
                payload = new StatusBase
                {
                    UserId = user.Id,
                    Handler = request.Handler,
                    Payload = request.ExternalPayload is not null 
                        ? _jsonSerializer.Deserialize<PaymentStatusData>(request.ExternalPayload) 
                        : null
                };
                break;
            case "video_started":
            case "video_ended":
                payload = new StatusBase
                {
                    UserId = user.Id,
                    Handler = request.Handler,
                    Payload = request.ExternalPayload is not null 
                        ? _jsonSerializer.Deserialize<ExternalData>(request.ExternalPayload) 
                        : null
                };
                break;
            case "chat_post_message": 
                payload = new StatusBase
                {
                    UserId = user.Id,
                    Handler = request.Handler,
                    Payload = request.ExternalPayload is not null 
                        ? _jsonSerializer.Deserialize<ChatData>(request.ExternalPayload) 
                        : null
                };
                break;
        }

        if (payload is null)
            throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        var data = _jsonSerializer.Serialize(payload, Formatting.None, Setting);
        if (!request.CanSkipPreservation)
            await SaveNotification(user.Id, data);

        if (request is { Handler: "chat_post_message", ExternalPayload: not null })
        {
            var chatData = _jsonSerializer.Deserialize<ChatData>(request.ExternalPayload);
            await _chatRepository.CreateChatCache(chatData.Id, chatData.ChatKey, data);
        }

        await _notificationService.Notify("WebNotificationGroup", data, request.Handler, cancellationToken);
        return new NotifyRequestCommandResult { StatusId = user.Id };
    }

    private async Task SaveNotification(Guid userId, string data)
    {
        var currentNotification = await _notificationRepository.GetWebNotificationById(userId);
        if (currentNotification is not null)
        {
            await  _notificationRepository.UpdateWebNotification(userId, data);
        }
        else
        {
            await _notificationRepository.CreateWebNotification(data, userId);
        }
    }
}