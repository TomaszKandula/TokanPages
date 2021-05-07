using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Middleware;
using TokanPages.Configuration;
using TokanPages.Backend.Database;
using TokanPages.Backend.Shared.Settings;

namespace TokanPages.Tests.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration AConfiguration, IWebHostEnvironment AEnvironment) 
            : base(AConfiguration, AEnvironment) { }

        public override void ConfigureServices(IServiceCollection AServices)
        {
            AServices
                .AddMvc(AOption => AOption.CacheProfiles
                    .Add("Standard", new CacheProfile
                    { 
                        Duration = 10, 
                        Location = ResponseCacheLocation.Any, 
                        NoStore = false 
                    }));

            AServices.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            AServices.AddControllers();
            AServices.AddResponseCompression(AOptions => AOptions.Providers.Add<GzipCompressionProvider>());

            Dependencies.RegisterForTests(AServices, FConfiguration);
        }

        public override void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            var LDatabaseContext = AApplication.ApplicationServices.GetRequiredService<DatabaseContext>();
            LDatabaseContext?.StartMigration();
            LDatabaseContext?.SeedData();

            AApplication.UseForwardedHeaders();
            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
            AApplication.UseMiddleware<GarbageCollector>();
            AApplication.UseMiddleware<CustomCors>();

            AApplication.UseResponseCompression();
            AApplication.UseHttpsRedirection();
            AApplication.UseStaticFiles();
            AApplication.UseRouting();

            AApplication.UseEndpoints(AEndpoints 
                => AEndpoints.MapControllers());
        }
    }
}