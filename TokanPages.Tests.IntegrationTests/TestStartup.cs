using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Middleware;
using TokanPages.Configuration;
using TokanPages.Backend.Database;
using TokanPages.Backend.Shared.Settings;
using TokanPages.Backend.Database.Initialize;

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

            SetupTestDatabase(AServices);
            Dependencies.CommonServices(AServices, FConfiguration);
        }

        public override void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            using var LServiceScope = AApplication.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var LTestDataSeeder = LServiceScope.ServiceProvider.GetService<IDbInitialize>();
            LTestDataSeeder.StartMigration();
            LTestDataSeeder.SeedData();
            
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

        private void SetupTestDatabase(IServiceCollection AServices)
        {
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(FConfiguration.GetConnectionString("DbConnectTest"));
                AOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                AOptions.EnableSensitiveDataLogging();
            });
        }
    }
}