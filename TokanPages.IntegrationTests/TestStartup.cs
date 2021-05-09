using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Middleware;
using TokanPages.Configuration;
using TokanPages.Backend.Database;
using TokanPages.Backend.Shared.Settings;

namespace TokanPages.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration AConfiguration, IWebHostEnvironment AEnvironment) 
            : base(AConfiguration, AEnvironment) { }

        public override void ConfigureServices(IServiceCollection AServices)
        {
            AServices.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            AServices.AddControllers();

            SetupTestDatabase(AServices);
            Dependencies.CommonServices(AServices, FConfiguration);
        }

        public override void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
            AApplication.UseMiddleware<CustomCors>();
            AApplication.UseForwardedHeaders();
            AApplication.UseHttpsRedirection();
            AApplication.UseRouting();
            AApplication.UseEndpoints(AEndpoints => AEndpoints.MapControllers());
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