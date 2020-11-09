using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace TokanPages
{

    public class Program
    {

        private static IWebHostBuilder CreateWebHostBuilder(string[] ARgs) =>
             WebHost.CreateDefaultBuilder(ARgs)
                 .UseStartup<Startup>()
                 .UseSerilog();

        public static int Main(string[] ARgs)
        {

            var LAppPath = AppDomain.CurrentDomain.BaseDirectory;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    LAppPath + "\\logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: null)
                .CreateLogger();

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
