using TokanPages.Services.PushNotificationService.Models;

namespace TokanPages.Services.PushNotificationService.Abstractions;

public interface IAzureNotificationHubUtility
{
    ConnectionStringData GetConnectionStringData(string connectionString);

    string GetSaSToken(TokenInput input);

    string MakeApsMessage(MessageInput input);

    string MakeFcmMessage(MessageInput input);
}