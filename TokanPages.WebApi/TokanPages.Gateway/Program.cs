using Logger = TokanPages.Backend.Configuration.Logger;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Serilog;

namespace TokanPages.Gateway;

/// <summary>
/// .NET6 program.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    private static readonly string? EnvironmentValue 
        = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    /// <summary>
    /// Main entry point
    /// </summary>
    /// <returns>Returns '1' on exception</returns>
    public static int Main()
    {
        try
        {
            var configuration = GetConfiguration();
            const string fileName = @"logs/TokanPages.Gateway/{yyyy}{MM}{dd}.txt";
            Log.Logger = Logger.Configuration.GetLogger(configuration, fileName);
            Log.Information("Starting WebHost... Environment: {Environment}", EnvironmentValue);
            CreateWebHostBuilder(configuration)
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

    private static IWebHostBuilder CreateWebHostBuilder(IConfigurationRoot configurationRoot)
    {
        return WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder => builder.AddConfiguration(configurationRoot))
            .ConfigureKestrel(options => options.AddServerHeader = false)
            .UseStartup<Startup>()
            .UseSerilog();
    }
}