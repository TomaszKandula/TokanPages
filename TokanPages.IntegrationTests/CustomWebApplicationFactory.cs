namespace TokanPages.IntegrationTests
{
    using System.Reflection;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class CustomWebApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
    {
        public string WebSecret { get; private set; }
        
        public string Issuer { get; private set; }
        
        public string Audience { get; private set; }

        public string Connection { get; private set; }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    var startupAssembly = typeof(TTestStartup).GetTypeInfo().Assembly;
                    var testConfig = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
                        .AddUserSecrets(startupAssembly)
                        .AddEnvironmentVariables()
                        .Build();

                    configurationBuilder.AddConfiguration(testConfig);

                    var config = configurationBuilder.Build();
                    Issuer = config.GetValue<string>("IdentityServer:Issuer");
                    Audience = config.GetValue<string>("IdentityServer:Audience");
                    WebSecret = config.GetValue<string>("IdentityServer:WebSecret");
                    Connection = config.GetValue<string>("ConnectionStrings:DbConnectTest");
                })
                .UseStartup<TTestStartup>()
                .UseTestServer();

            return builder;
        }
    }
}