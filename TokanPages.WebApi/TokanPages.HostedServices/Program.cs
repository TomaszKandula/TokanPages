using System.Diagnostics.CodeAnalysis;
using Serilog;
using TokanPages.Backend.Configuration;

namespace TokanPages.HostedServices;

/// <summary>
/// Program.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    private static readonly string? EnvironmentValue 
        = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    /// <summary>
    /// Main entry point.
    /// </summary>
    /// <returns>Returns '1' on exception.</returns>
    public static int Main()
    {
        try
        {
            var configuration = GetConfiguration();
            var isProduction = EnvironmentValue == "Production";
            const string fileName = @"logs/TokanPages.HostedServices/{yyyy}{MM}{dd}.txt";

            Log.Logger = SeriLogSupport.GetLogger(configuration, fileName, isProduction);
            Log.Information("Starting WebHost... Environment: {Environment}", EnvironmentValue);

            CreateHostBuilder(configuration)
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureKestrel(options => options.AddServerHeader = false)
                    .UseStartup<Startup>())
                .Build()
                .Run();

            return 0;
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "WebHost has been terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IConfigurationRoot GetConfiguration()
    {
        var appSettingsEnv = $"appsettings.{EnvironmentValue}.json";
        return new ConfigurationBuilder()
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<Startup>(true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static IHostBuilder CreateHostBuilder(IConfigurationRoot configurationRoot)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder => builder.AddConfiguration(configurationRoot))
            .UseSerilog();
    }
}
