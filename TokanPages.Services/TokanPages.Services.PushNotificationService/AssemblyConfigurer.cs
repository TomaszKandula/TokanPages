using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Shared.Options;
using TokanPages.Services.PushNotificationService.Abstractions;

namespace TokanPages.Services.PushNotificationService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddAzureNotificationHub(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAzureNotificationHubUtility, AzureNotificationHubUtility>();

        var settings = configuration.GetAppSettings();
        services.AddSingleton<IAzureNotificationHubFactory>(_ =>
        {
            var containerName = settings.AzHubHubName;
            var connectionString = settings.AzHubConnectionString;
            return new AzureNotificationHubFactory(containerName, connectionString);
        });
    }
}