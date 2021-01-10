using System;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Backend.IntegrationTests
{

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override IWebHostBuilder CreateWebHostBuilder()
        {

            var LBuilder = WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration(AConfig =>
                {

                    var LEnvironment = "IntegrationTest";
                    Environment.SetEnvironmentVariable("ASPNETCORE_STAGING", LEnvironment);

                    var LStartupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
                    var LTestConfig = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
                        .AddUserSecrets(LStartupAssembly)
                        .AddEnvironmentVariables()
                        .Build();

                    AConfig.AddConfiguration(LTestConfig);

                })
                .UseStartup<TStartup>()
                .UseTestServer();

            return LBuilder;

        }

    }

}
