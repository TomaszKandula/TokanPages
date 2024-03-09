using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Tests.EndToEndTests.Helpers;

[UsedImplicitly]
public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    public IConfiguration? Configuration { get; private set; }

    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        var builder = WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(configurationBuilder =>
            {
                var environment = Persistence.MigrationRunner.Helpers.Environments.CurrentValue;
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{environment}.json", true, true)
                    .AddUserSecrets<T>(true)
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(configuration);
                Configuration = configurationBuilder.Build();
            })
            .UseStartup<T>()
            .UseTestServer();

        return builder;
    }
}