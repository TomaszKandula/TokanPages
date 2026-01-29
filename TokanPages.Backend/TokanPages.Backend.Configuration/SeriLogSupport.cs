using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace TokanPages.Backend.Configuration;

[ExcludeFromCodeCoverage]
public static class SeriLogSupport
{
    private const string LogTemplate 
        = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static ILogger GetLogger(IConfigurationRoot configuration, string storageFileName, bool isProduction)
    {
        var connectionString = configuration.GetValue<string>("AZ_Storage_ConnectionString");
        var containerName = configuration.GetValue<string>("AZ_Storage_ContainerName");

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