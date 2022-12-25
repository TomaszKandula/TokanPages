using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Tests.EndToEndTests.Helpers;

[UsedImplicitly]
public class CustomWebApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
{
    public IConfiguration? Configuration { get; private set; }

    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        var builder = WebHost.CreateDefaultBuilder()
             .ConfigureAppConfiguration(configurationBuilder =>
             {
                 var startupAssembly = typeof(TTestStartup).GetTypeInfo().Assembly;
                 var testConfig = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.Testing.json", true, true)
                     .AddUserSecrets(startupAssembly, true)
                     .AddEnvironmentVariables()
                     .Build();
            
                 configurationBuilder.AddConfiguration(testConfig);
                 Configuration = configurationBuilder.Build();
             })
            .UseStartup<TTestStartup>()
            .UseTestServer();

        return builder;
    }
}