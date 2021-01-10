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

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration AConfiguration)
        {
            Configuration = AConfiguration;
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

            AServices.AddMvc(AOption => AOption.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            Dependencies.Register(AServices, Configuration);

            AServices.AddControllers();
            
            AServices.AddSpaStaticFiles(configuration => 
            { 
                configuration.RootPath = "ClientApp/build"; 
            });
            
            AServices.AddResponseCompression(AOptions => 
            { 
                AOptions.Providers.Add<GzipCompressionProvider>(); 
            });
            
            AServices.AddSwaggerGen(AOption => 
            { 
                AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "TokanPagesApi", Version = "v1" }); 
            });

        }

        public void Configure(IApplicationBuilder AApplication, IWebHostEnvironment AEnvironment, AppUrls AAppUrls)
        {

            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
            AApplication.UseMiddleware<GarbageCollector>();
            AApplication.UseMiddleware<CustomCors>();

            AApplication.UseResponseCompression();
            AApplication.UseHttpsRedirection();
            AApplication.UseStaticFiles();
            AApplication.UseSpaStaticFiles();
            AApplication.UseRouting();

            AApplication.UseSwagger();
            AApplication.UseSwaggerUI(AOption =>
            {
                AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPagesApi version 1");
            });

            AApplication.UseEndpoints(AEndpoints =>
            {
                AEndpoints.MapControllers();
            });

            AApplication.UseSpa(ASpa =>
            {
                
                ASpa.Options.SourcePath = "ClientApp";

                if (AEnvironment.IsDevelopment())
                {
                    ASpa.UseProxyToSpaDevelopmentServer(AAppUrls.DevelopmentOrigin);
                }

            });           

        }

    }

}
