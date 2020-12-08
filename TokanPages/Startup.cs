using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.AppLogger;
using TokanPages.BackEnd.Middleware;

using TokanPages.Backend.Database;
using TokanPages.Backend.Database.Settings;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.SmtpClient.Settings;

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

            AServices.AddControllers();

            AServices.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            AServices.AddMvc(AOption => AOption.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            AServices.AddSingleton(Configuration.GetSection("AzureStorage").Get<AzureStorageSettings>());
            AServices.AddSingleton(Configuration.GetSection("SmtpServer").Get<SmtpServerSettings>());
            AServices.AddSingleton(Configuration.GetSection("CosmosDb").Get<CosmosDbSettings>());

            AServices.AddSingleton<IAppLogger, AppLogger>();

            AServices.AddScoped<ISmtpClientService, SmtpClientService>();          
            AServices.AddScoped<IAzureStorageService, AzureStorageService>();
            AServices.AddScoped<ICosmosDbService, CosmosDbService>();

            AServices.AddScoped<ILogicContext, LogicContext>();

            AServices.AddResponseCompression(AOptions =>
            {
                AOptions.Providers.Add<GzipCompressionProvider>();
            });

            AServices.AddSwaggerGen(AOption =>
            {
                AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "TokanPagesApi", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder AApplication, IWebHostEnvironment AEnvironment)
        {

            AApplication.UseResponseCompression();
            AApplication.UseMiddleware<GarbageCollector>();
            AApplication.UseMiddleware<CustomCors>();

            if (AEnvironment.IsDevelopment())
            {
                AApplication.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. 
                // You may want to change this for production scenarios, 
                // see https://aka.ms/aspnetcore-hsts.
                AApplication.UseHsts();
            }

            AApplication.UseSwagger();
            AApplication.UseSwaggerUI(AOption =>
            {
                AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPagesApi version 1");
            });

            AApplication.UseStaticFiles();
            AApplication.UseSpaStaticFiles();
            AApplication.UseRouting();

            AApplication.UseEndpoints(AEndpoints =>
            {
                AEndpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            AApplication.UseSpa(ASpa =>
            {
                
                ASpa.Options.SourcePath = "ClientApp";

                if (AEnvironment.IsDevelopment())
                {
                    ASpa.UseProxyToSpaDevelopmentServer(Configuration.GetSection("DevelopmentOrigin").Value);
                }

            });           

        }

    }

}
