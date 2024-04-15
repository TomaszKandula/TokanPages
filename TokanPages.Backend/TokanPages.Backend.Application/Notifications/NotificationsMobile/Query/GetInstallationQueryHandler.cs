using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PushNotificationService.Abstractions;

namespace TokanPages.Backend.Application.NotificationsMobile.Query;

public class GetInstallationQueryHandler : RequestHandler<GetInstallationQuery, GetInstallationQueryResult>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public GetInstallationQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory) : base(databaseContext, loggerService)
        => _azureNotificationHubFactory = azureNotificationHubFactory;

    public override async Task<GetInstallationQueryResult> Handle(GetInstallationQuery request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        var installation = await hub.GetInstallationById(request.Id.ToString(), cancellationToken);

        DateTime? dateTime = null;
        if (!string.IsNullOrEmpty(installation.ExpirationTime))
            dateTime = DateTime.Parse(installation.ExpirationTime);

        return new GetInstallationQueryResult
        {
            InstallationId = installation.InstallationId,
            ExpirationTime = dateTime,
            Platform = installation.Platform,
            Tags = installation.Tags
        };
    }
}