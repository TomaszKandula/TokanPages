using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
                .UseSerilog((Context, Config) => 
                {
                    Config.ReadFrom.Configuration(Context.Configuration);
                    Config.WriteTo.Sentry(Sentry => 
                    {
                        Sentry.SendDefaultPii = true;
                        Sentry.MinimumBreadcrumbLevel = LogEventLevel.Information;
                        Sentry.MinimumEventLevel = LogEventLevel.Information;
                        Sentry.AttachStacktrace = true;
                        Sentry.Debug = true;
                        Sentry.DiagnosticsLevel = SentryLevel.Error;
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
                Log.Fatal(LException, "WebHost has been terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
