using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Database.Initialize;
using Sentry.Protocol;
using Serilog.Events;
using Serilog;

namespace TokanPages
{
    public static class Program
    {
        private static readonly bool FIsDevelopment 
            = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
        
        public static int Main(string[] AParams)
        {
            try
            {
                Log.Information("Starting WebHost...");
                CreateWebHostBuilder(AParams)
                    .Build()
                    .MigrateDatabase()
                    .Run();
                return 0;
            }
            catch (Exception LException)
            {
                Log.Fatal(LException, "WebHost has been terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] AParams)
        {
            return WebHost.CreateDefaultBuilder(AParams)
                .UseStartup<Startup>()
                .UseSerilog((AContext, AConfig) =>
                {
                    AConfig.ReadFrom.Configuration(AContext.Configuration);
                    AConfig.WriteTo.Console();
                    AConfig.MinimumLevel.Information();
                    AConfig.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    AConfig.Enrich.FromLogContext();
                    AConfig.WriteTo.Sentry(ASentry =>
                    {
                        ASentry.SendDefaultPii = true;
                        ASentry.MinimumBreadcrumbLevel = LogEventLevel.Information;
                        ASentry.MinimumEventLevel = LogEventLevel.Information;
                        ASentry.AttachStacktrace = true;
                        ASentry.Debug = true;
                        ASentry.DiagnosticsLevel = SentryLevel.Error;
                    });
                })
                .UseSentry();
        }

        private static IWebHost MigrateDatabase(this IWebHost AWebHost)
        {
            var LServiceScopeFactory = (IServiceScopeFactory) AWebHost.Services.GetService(typeof(IServiceScopeFactory));
            using var LScope = LServiceScopeFactory.CreateScope();
            var LServices = LScope.ServiceProvider;
            var LDbInitialize = LServices.GetRequiredService<IDbInitialize>();

            if (!FIsDevelopment) 
                return AWebHost;

            LDbInitialize.StartMigration();
            LDbInitialize.SeedData();

            return AWebHost;
        }
    }
}