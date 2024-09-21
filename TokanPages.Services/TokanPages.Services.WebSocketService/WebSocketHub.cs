using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;

namespace TokanPages.Services.WebSocketService;

[ExcludeFromCodeCoverage]
public class WebSocketHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "WebNotificationGroup");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}