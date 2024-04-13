namespace TokanPages.Services.PushNotificationService.Abstractions;

public interface IAzureNotificationHubFactory
{
    IAzureNotificationHubClient Create();
}