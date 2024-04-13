using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PushNotificationService.Abstractions;

namespace TokanPages.Backend.Application.NotificationsMobile.Query;

public class GetAllInstallationsQueryHandler : RequestHandler<GetAllInstallationsQuery, GetAllInstallationsQueryResult>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public GetAllInstallationsQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory) : base(databaseContext, loggerService)
        => _azureNotificationHubFactory = azureNotificationHubFactory;

    public override async Task<GetAllInstallationsQueryResult> Handle(GetAllInstallationsQuery request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        var installations = await hub.GetAllInstallations(cancellationToken);
        LoggerService.LogInformation($"Returned {installations.Count} installations");

        return new GetAllInstallationsQueryResult { Installations = installations };
    }
}