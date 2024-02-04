using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace TokanPages.Backend.Configuration.Logger;

public static class Configuration
{
    private const string LogTemplate 
        = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static ILogger GetLogger(IConfigurationRoot configuration, string storageFileName)
    {
        var connectionString = configuration.GetValue<string>("AZ_Storage_ConnectionString");
        var containerName = configuration.GetValue<string>("AZ_Storage_ContainerName");

        return new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.AzureBlobStorage(
                connectionString: connectionString, 
                restrictedToMinimumLevel: LogEventLevel.Information,
                storageContainerName: containerName,
                storageFileName: storageFileName,
                writeInBatches: true,
                period: TimeSpan.FromSeconds(30),
                outputTemplate: LogTemplate)
            .CreateLogger();
    }
}