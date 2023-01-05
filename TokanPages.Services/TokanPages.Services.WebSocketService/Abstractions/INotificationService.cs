namespace TokanPages.Services.WebSocketService.Abstractions;

public interface INotificationService
{
    Task Notify(string id, object data, string handler, CancellationToken cancellationToken = default);
}