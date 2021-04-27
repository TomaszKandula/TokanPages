using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Sentry.Protocol;

namespace TokanPages
{
    public class Program
    {
        private static IWebHostBuilder CreateWebHostBuilder(string[] ARgs) =>
            WebHost.CreateDefaultBuilder(ARgs)
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

        public static int Main(string[] ARgs)
        {
            try
            {
                Log.Information("Starting WebHost...");
                CreateWebHostBuilder(ARgs).Build().Run();
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
    }
}
