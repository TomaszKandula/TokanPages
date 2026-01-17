using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PushNotificationService.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class DeleteInstallationCommandHandler : RequestHandler<DeleteInstallationCommand, Unit>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public DeleteInstallationCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory) : base(operationDbContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
    }

    public override async Task<Unit> Handle(DeleteInstallationCommand request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        await hub.DeleteInstallationById(request.Id.ToString(), cancellationToken);
        LoggerService.LogInformation($"Installation ({request.Id}) has been removed from Azure Notification Hub.");

        var notificationTags = await OperationDbContext.PushNotificationTags
            .Where(tags => tags.PushNotificationId == request.Id)
            .ToListAsync(cancellationToken);

        if (notificationTags.Count > 0)
        {
            OperationDbContext.RemoveRange(notificationTags);
            LoggerService.LogInformation("Related push notification tags have been removed.");
        }

        var notification = await OperationDbContext.PushNotifications
            .Where(notifications => notifications.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (notification is not null)
        {
            OperationDbContext.Remove(notification);
            LoggerService.LogInformation("Installation record has been removed from database.");
        }

        await OperationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}