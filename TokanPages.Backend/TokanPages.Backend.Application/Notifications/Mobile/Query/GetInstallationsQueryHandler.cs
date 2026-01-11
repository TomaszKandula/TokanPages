using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.PushNotificationService.Abstractions;

namespace TokanPages.Backend.Application.Notifications.Mobile.Query;

public class GetInstallationsQueryHandler : RequestHandler<GetInstallationsQuery, GetInstallationsQueryResult>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public GetInstallationsQueryHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory) : base(operationsDbContext, loggerService)
        => _azureNotificationHubFactory = azureNotificationHubFactory;

    public override async Task<GetInstallationsQueryResult> Handle(GetInstallationsQuery request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        var installations = await hub.GetAllInstallations(cancellationToken);
        LoggerService.LogInformation($"Returned {installations.Count} installations");

        return new GetInstallationsQueryResult { Installations = installations };
    }
}