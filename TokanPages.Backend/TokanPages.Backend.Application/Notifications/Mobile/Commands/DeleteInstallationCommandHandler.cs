using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PushNotificationService.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class DeleteInstallationCommandHandler : RequestHandler<DeleteInstallationCommand, Unit>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public DeleteInstallationCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory) : base(operationsDbContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
    }

    public override async Task<Unit> Handle(DeleteInstallationCommand request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        await hub.DeleteInstallationById(request.Id.ToString(), cancellationToken);
        LoggerService.LogInformation($"Installation ({request.Id}) has been removed from Azure Notification Hub.");

        var notificationTags = await OperationsDbContext.PushNotificationTags
            .Where(tags => tags.PushNotificationId == request.Id)
            .ToListAsync(cancellationToken);

        if (notificationTags.Count > 0)
        {
            OperationsDbContext.RemoveRange(notificationTags);
            LoggerService.LogInformation("Related push notification tags have been removed.");
        }

        var notification = await OperationsDbContext.PushNotifications
            .Where(notifications => notifications.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (notification is not null)
        {
            OperationsDbContext.Remove(notification);
            LoggerService.LogInformation("Installation record has been removed from database.");
        }

        await OperationsDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}