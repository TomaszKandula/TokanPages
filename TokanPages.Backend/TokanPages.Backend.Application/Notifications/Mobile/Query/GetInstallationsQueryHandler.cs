using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.PushNotificationService.Abstractions;

namespace TokanPages.Backend.Application.Notifications.Mobile.Query;

public class GetInstallationsQueryHandler : RequestHandler<GetInstallationsQuery, GetInstallationsQueryResult>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public GetInstallationsQueryHandler(ILoggerService loggerService, IAzureNotificationHubFactory azureNotificationHubFactory) 
        : base(loggerService) => _azureNotificationHubFactory = azureNotificationHubFactory;

    public override async Task<GetInstallationsQueryResult> Handle(GetInstallationsQuery request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        var installations = await hub.GetAllInstallations(cancellationToken);
        LoggerService.LogInformation($"Returned {installations.Count} installations");

        return new GetInstallationsQueryResult { Installations = installations };
    }
}