using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.WebSocketService.Abstractions;

namespace TokanPages.Backend.Application.NotificationsWeb.Command;

public class NotifyRequestCommandHandler : RequestHandler<NotifyRequestCommand, Unit>
{
    private readonly INotificationService _notificationService;

    public NotifyRequestCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        INotificationService notificationService) : base(databaseContext, loggerService)
    {
        _notificationService = notificationService;
    }

    public override async Task<Unit> Handle(NotifyRequestCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload ?? new { };
        await _notificationService.Notify("WebNotificationGroup", payload, request.Handler, cancellationToken);
        return Unit.Value;
    }
}