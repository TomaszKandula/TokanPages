using TokanPages.Backend.Application.Notifications.Web.Models;
using TokanPages.Backend.Application.Notifications.Web.Models.Base;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Notification;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.WebSocketService.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Backend.Application.Notifications.Web.Command;

public class NotifyRequestCommandHandler : RequestHandler<NotifyRequestCommand, NotifyRequestCommandResult>
{
    private readonly INotificationService _notificationService;

    private readonly IJsonSerializer _jsonSerializer;

    private static JsonSerializerSettings Setting => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    public NotifyRequestCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        INotificationService notificationService, IJsonSerializer jsonSerializer) : base(databaseContext, loggerService)
    {
        _notificationService = notificationService;
        _jsonSerializer = jsonSerializer;
    }

    public override async Task<NotifyRequestCommandResult> Handle(NotifyRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await DatabaseContext.Users
            .AsNoTracking()
            .Where(user => user.Id == request.UserId)
            .SingleOrDefaultAsync(cancellationToken);

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
            await SaveNotification(user.Id, data, cancellationToken);

        if (request is { Handler: "chat_post_message", ExternalPayload: not null })
        {
            var chatData = _jsonSerializer.Deserialize<ChatData>(request.ExternalPayload);
            var cache = new UserMessageCache
            {
                Id = chatData.Id,
                ChatKey = chatData.ChatKey,
                Notification = data
            };
            
            await DatabaseContext.UserMessagesCache.AddAsync(cache, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        await _notificationService.Notify("WebNotificationGroup", data, request.Handler, cancellationToken);
        return new NotifyRequestCommandResult { StatusId = user.Id };
    }

    private async Task SaveNotification(Guid userId, string data, CancellationToken cancellationToken)
    {
        var currentNotification = await DatabaseContext.WebNotifications
            .Where(notifications => notifications.Id == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (currentNotification is not null)
        {
            currentNotification.Value = data;
        }
        else
        {
            var webNotification = new WebNotification { Id = userId, Value = data };
            await DatabaseContext.WebNotifications.AddAsync(webNotification, cancellationToken);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
    }
}