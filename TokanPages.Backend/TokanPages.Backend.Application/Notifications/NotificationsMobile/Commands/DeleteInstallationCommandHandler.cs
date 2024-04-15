using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PushNotificationService.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.NotificationsMobile.Commands;

public class DeleteInstallationCommandHandler : RequestHandler<DeleteInstallationCommand, Unit>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public DeleteInstallationCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory) : base(databaseContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
    }

    public override async Task<Unit> Handle(DeleteInstallationCommand request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        await hub.DeleteInstallationById(request.Id.ToString(), cancellationToken);
        LoggerService.LogInformation($"Installation ({request.Id}) has been removed from Azure Notification Hub.");

        var notificationTags = await DatabaseContext.PushNotificationTags
            .Where(tags => tags.PushNotificationId == request.Id)
            .ToListAsync(cancellationToken);

        if (notificationTags.Any())
        {
            DatabaseContext.RemoveRange(notificationTags);
            LoggerService.LogInformation("Related push notification tags have been removed.");
        }

        var notification = await DatabaseContext.PushNotifications
            .Where(notifications => notifications.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (notification is not null)
        {
            DatabaseContext.Remove(notification);
            LoggerService.LogInformation("Installation record has been removed from database.");
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}