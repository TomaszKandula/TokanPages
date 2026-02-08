using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Shared.Options;
using TokanPages.Services.AzureBusService.Abstractions;

namespace TokanPages.Services.AzureBusService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddAzureBus(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        services.AddSingleton<IAzureBusFactory>(_ =>
        {
            var connectionString = settings.AzBusConnectionString;
            return new AzureBusFactory(connectionString);
        });
    }
}