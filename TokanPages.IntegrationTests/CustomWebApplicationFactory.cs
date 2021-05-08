using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using JetBrains.Annotations;

namespace TokanPages.IntegrationTests
{
    [UsedImplicitly]
    public class CustomWebApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            // var test = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // var x = (typeof(TTestStartup).GetTypeInfo().Assembly).CodeBase;
            //var test = Environment.CurrentDirectory;
            var LBuilder = WebHost.CreateDefaultBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSolutionRelativeContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(AConfig =>
                {
                    var LStartupAssembly = typeof(TTestStartup).GetTypeInfo().Assembly;
                    var LTestConfig = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
                        .AddUserSecrets(LStartupAssembly)
                        .AddEnvironmentVariables()
                        .Build();
                
                    AConfig.AddConfiguration(LTestConfig);
                })
                .UseStartup<TTestStartup>()
                .UseTestServer();
            
            return LBuilder;
        }
    }
}