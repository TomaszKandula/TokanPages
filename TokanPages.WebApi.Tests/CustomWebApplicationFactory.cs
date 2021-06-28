using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using JetBrains.Annotations;

namespace TokanPages.WebApi.Tests
{
    [UsedImplicitly]
    public class CustomWebApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
    {
        public string WebSecret { get; private set; }
        
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var LBuilder = WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration(AConfig =>
                {
                    var LStartupAssembly = typeof(TTestStartup).GetTypeInfo().Assembly;
                    var LTestConfig = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
                        .AddUserSecrets(LStartupAssembly)
                        .AddEnvironmentVariables()
                        .Build();
                
                    AConfig.AddConfiguration(LTestConfig);
                    var LConfig = AConfig.Build();
                    WebSecret = LConfig.GetValue<string>("IdentityServer:WebSecret");
                })
                .UseStartup<TTestStartup>()
                .UseTestServer();
            
            return LBuilder;
        }
    }
}