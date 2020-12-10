using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace BackEnd.IntegrationTests.Configuration
{

    public class TestFixture<TStartup> : IDisposable
    {

        public TestServer FServer { get; }
        public HttpClient FClient { get; }

        public void Dispose()
        {
            FClient.Dispose();
            FServer.Dispose();
        }

        private static string GetProjectPath(string AprojectRelativePath, Assembly AStartupAssembly)
        {

            var LProjectName = AStartupAssembly.GetName().Name;
            var LApplicationBasePath = AppContext.BaseDirectory;
            var LDirectoryInfo = new DirectoryInfo(LApplicationBasePath);

            do
            {

                LDirectoryInfo = LDirectoryInfo.Parent;

                var LProjectDirectoryInfo = new DirectoryInfo(Path.Combine(LDirectoryInfo.FullName, AprojectRelativePath));

                if (LProjectDirectoryInfo.Exists)
                {

                    if (new FileInfo(Path.Combine(LProjectDirectoryInfo.FullName, LProjectName, $"{LProjectName}.csproj")).Exists)
                    {
                        return Path.Combine(LProjectDirectoryInfo.FullName, LProjectName);
                    }

                }

            }
            while (LDirectoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {LApplicationBasePath}.");

        }

        protected virtual void InitializeServices(IServiceCollection AServices)
        {

            var LStartupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            var LManager = new ApplicationPartManager
            {

                ApplicationParts =
                {
                    new AssemblyPart(LStartupAssembly)
                },

                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider()
                }

            };

            AServices.AddSingleton(LManager);

        }

        public TestFixture() : this(Path.Combine(""))
        {
        }

        protected TestFixture(string ARelativeTargetProjectParentDir)
        {

            var LStartupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var LContentRoot = GetProjectPath(ARelativeTargetProjectParentDir, LStartupAssembly);

            var LConfigurationBuilder = new ConfigurationBuilder()
                .SetBasePath(LContentRoot)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(LStartupAssembly);

            var LWebHostBuilder = new WebHostBuilder()
                .UseContentRoot(LContentRoot)
                .ConfigureServices(InitializeServices)
                .UseConfiguration(LConfigurationBuilder.Build())
                .UseStartup(typeof(TStartup));

            // Create instance of test server
            FServer = new TestServer(LWebHostBuilder);

            // Add configuration for client
            FClient = FServer.CreateClient();
            FClient.BaseAddress = new Uri("http://localhost:5000");
            FClient.DefaultRequestHeaders.Accept.Clear();
            FClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

    }

}
