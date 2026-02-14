using TokanPages.Backend.Application.Notifications.Web.Models.Base;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Notification;

namespace TokanPages.Backend.Application.Notifications.Web.Command;

public class StatusRequestCommandHandler : RequestHandler<StatusRequestCommand, StatusRequestCommandResult>
{
    private readonly IJsonSerializer _jsonSerializer;

    private readonly INotificationRepository _notificationRepository;

    public StatusRequestCommandHandler(ILoggerService loggerService, IJsonSerializer jsonSerializer, 
        INotificationRepository notificationRepository) : base(loggerService)
    {
        _jsonSerializer = jsonSerializer;
        _notificationRepository = notificationRepository;
    }

    public override async Task<StatusRequestCommandResult> Handle(StatusRequestCommand request, CancellationToken cancellationToken)
    {
        var webNotification = await _notificationRepository.GetWebNotificationById(request.StatusId);
        if (webNotification is null)
            return new StatusRequestCommandResult();

        var data = _jsonSerializer.Deserialize<StatusBase>(webNotification.Value);
        var result = new StatusRequestCommandResult
        {
            UserId = data.UserId,
            Handler = data.Handler,
            Payload = data.Payload
        };

        await _notificationRepository.DeleteWebNotificationById(request.StatusId);
        return result;
    }
}