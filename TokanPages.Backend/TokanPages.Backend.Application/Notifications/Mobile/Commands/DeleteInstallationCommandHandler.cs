using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.PushNotificationService.Abstractions;
using MediatR;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Notification;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class DeleteInstallationCommandHandler : RequestHandler<DeleteInstallationCommand, Unit>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    private readonly INotificationRepository _notificationRepository;

    public DeleteInstallationCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory, INotificationRepository notificationRepository) : base(operationDbContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
        _notificationRepository = notificationRepository;
    }

    public override async Task<Unit> Handle(DeleteInstallationCommand request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        await hub.DeleteInstallationById(request.Id.ToString(), cancellationToken);
        LoggerService.LogInformation($"Installation ({request.Id}) has been removed from Azure Notification Hub.");

        await _notificationRepository.DeletePushNotificationTagsById(request.Id);
        await _notificationRepository.DeletePushNotificationByPk(request.Id);

        LoggerService.LogInformation("Installation record has been removed from database.");
        return Unit.Value;
    }
}