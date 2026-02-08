using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Services.WebSocketService.Abstractions;

namespace TokanPages.Services.WebSocketService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddWebSocketService(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService<WebSocketHub>>();
    }
}