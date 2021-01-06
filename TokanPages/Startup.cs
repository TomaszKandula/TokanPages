using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using TokanPages.Middleware;
using TokanPages.Configuration;
using TokanPages.CustomHandlers;
using TokanPages.Backend.Shared.Settings;

namespace TokanPages
{

    public class Startup
    {

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }
        private bool IsIntegrationTesting { get; }
            = System.Environment.GetEnvironmentVariable("ASPNETCORE_WEBAPPLICATIONFACTORY") == "IntegrationTest";

        public Startup(IConfiguration AConfiguration, IWebHostEnvironment AEnvironment)
        {
            Configuration = AConfiguration;
            Environment = AEnvironment;
        }

        public void ConfigureServices(IServiceCollection AServices)
        {

            AServices.AddMvc(AOption => AOption.CacheProfiles
                .Add("Standard", new CacheProfile() 
                { 
                    Duration = 10, 
                    Location = ResponseCacheLocation.Any, 
                    NoStore = false 
                }));

            AServices.AddMvc();
            AServices.AddControllers();          
            AServices.AddSpaStaticFiles(AConfiguration => { AConfiguration.RootPath = "ClientApp/build"; });
            AServices.AddResponseCompression(AOptions => { AOptions.Providers.Add<GzipCompressionProvider>(); });

            if (IsIntegrationTesting)
            {
                Dependencies.RegisterForTests(AServices, Configuration);
            }
            else 
            {
                Dependencies.Register(AServices, Configuration);
            }

            if (Environment.IsDevelopment()) 
            {
                AServices.AddSwaggerGen(AOption =>
                {
                    AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "TokanPagesApi", Version = "v1" });
                });
            }

        }

        public void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {

            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
            AApplication.UseMiddleware<GarbageCollector>();
            AApplication.UseMiddleware<CustomCors>();

            AApplication.UseResponseCompression();
            AApplication.UseHttpsRedirection();
            AApplication.UseStaticFiles();
            AApplication.UseSpaStaticFiles();
            AApplication.UseRouting();

            if (Environment.IsDevelopment())
            {
                AApplication.UseSwagger();
                AApplication.UseSwaggerUI(AOption =>
                {
                    AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPagesApi version 1");
                });
            }

            AApplication.UseEndpoints(AEndpoints =>
            {
                AEndpoints.MapControllers();
            });

            AApplication.UseSpa(ASpa =>
            {
                
                ASpa.Options.SourcePath = "ClientApp";

                if (Environment.IsDevelopment())
                {
                    ASpa.UseProxyToSpaDevelopmentServer(AAppUrls.DevelopmentOrigin);
                }

            });           

        }

    }

}
