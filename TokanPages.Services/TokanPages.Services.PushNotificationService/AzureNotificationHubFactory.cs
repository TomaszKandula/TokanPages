using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PushNotificationService.Abstractions;

namespace TokanPages.Services.PushNotificationService;

[ExcludeFromCodeCoverage]
public class AzureNotificationHubFactory : IAzureNotificationHubFactory
{
    private readonly string _hubName;

    private readonly string _connectionString;

    public AzureNotificationHubFactory(string hubName, string connectionString)
    {
        _hubName = hubName;
        _connectionString = connectionString;
    }

    public IAzureNotificationHubClient Create()
        => new AzureNotificationHubClient(_hubName, _connectionString);
}