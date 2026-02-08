using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Shared.Options;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Services.AzureStorageService;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddAzureStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetAppSettings();
        services.AddSingleton<IAzureBlobStorageFactory>(_ =>
        {
            var containerName = settings.AzStorageContainerName;
            var connectionString = settings.AzStorageConnectionString;
            return new AzureBlobStorageFactory(connectionString, containerName);
        });
    }
}