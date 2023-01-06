using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Serilog;
using Serilog.Events;

namespace TokanPages.WebApi;

/// <summary>
/// Program.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    private static readonly string? EnvironmentValue 
        = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private const string LogTemplate 
        = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    /// <summary>
    /// Main entry point.
    /// </summary>
    /// <param name="args">Argument array.</param>
    /// <returns>Integer.</returns>
    public static int Main(string[] args)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    GetLogPathFile(),
                    outputTemplate: LogTemplate,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: null,
                    shared: false
                ).CreateLogger();

            Log.Information("Starting WebHost...");
            Log.Information("Environment: {Environment}", EnvironmentValue);

            CreateWebHostBuilder(args)
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

    private static string GetLogPathFile()
    {
        var pathFolder = $"{AppDomain.CurrentDomain.BaseDirectory}logs";

        if (!Directory.Exists(pathFolder))
            Directory.CreateDirectory(pathFolder);

        return $"{pathFolder}{Path.DirectorySeparatorChar}log-.txt";
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog();
    }
}