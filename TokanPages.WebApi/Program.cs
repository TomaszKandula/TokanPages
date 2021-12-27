namespace TokanPages.WebApi;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Backend.Database.Initializer;
using Serilog.Events;
using Serilog;
using Sentry;

[ExcludeFromCodeCoverage]
public static class Program
{
    private static readonly bool FIsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
        
    public static int Main(string[] args)
    {
        try
        {
            Log.Information("Starting WebHost...");
            CreateWebHostBuilder(args)
                .Build()
                .MigrateDatabase()
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

    private static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
                config.WriteTo.Console();
                config.MinimumLevel.Information();
                config.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                config.Enrich.FromLogContext();
                config.WriteTo.Sentry(sentry =>
                {
                    sentry.SendDefaultPii = true;
                    sentry.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                    sentry.MinimumEventLevel = LogEventLevel.Warning;
                    sentry.AttachStacktrace = true;
                    sentry.Debug = true;
                    sentry.DiagnosticLevel = SentryLevel.Error;
                });
            })
            .UseSentry();
    }

    private static IWebHost MigrateDatabase(this IWebHost webHost)
    {
        var serviceScopeFactory = (IServiceScopeFactory) webHost.Services.GetService(typeof(IServiceScopeFactory));
        if (serviceScopeFactory == null) 
            return webHost;
            
        using var scope = serviceScopeFactory.CreateScope();
        var services = scope.ServiceProvider;
        var dbInitializer = services.GetRequiredService<IDbInitializer>();

        if (!FIsDevelopment) 
            return webHost;

        dbInitializer.StartMigration();
        dbInitializer.SeedData();

        return webHost;
    }
}