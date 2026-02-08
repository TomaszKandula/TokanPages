using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;
using TokanPages.Backend.Shared.Options;

namespace TokanPages.Backend.Configuration;

[ExcludeFromCodeCoverage]
internal static class SeriLogSupport
{
    private const string LogTemplate 
        = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    internal static ILogger GetLogger(IConfigurationRoot configuration, string storageFileName, bool isProduction)
    {
        var settings = configuration.GetAppSettings();
        var connectionString = settings.AzStorageConnectionString;
        var containerName = settings.AzStorageContainerName;

        var logger = isProduction 
            ? new LoggerConfiguration().MinimumLevel.Information() 
            : new LoggerConfiguration().MinimumLevel.Debug();

        return logger
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.AzureBlobStorage(
                connectionString: connectionString, 
                storageContainerName: containerName,
                storageFileName: storageFileName,
                writeInBatches: true,
                period: TimeSpan.FromSeconds(30),
                outputTemplate: LogTemplate)
            .CreateLogger();
    }
}