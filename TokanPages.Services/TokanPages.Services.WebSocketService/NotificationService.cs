using TokanPages.Services.WebSocketService.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace TokanPages.Services.WebSocketService;

public class NotificationService<T> : INotificationService where T : Hub
{
    private readonly IHubContext<T> _hubContext;

    public NotificationService(IHubContext<T> hubContext) => _hubContext = hubContext;

    public async Task Notify(string id, object data, string handler, CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.Group(id).SendAsync(handler, data, cancellationToken);
    }
}