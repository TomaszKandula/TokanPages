using System.Net;
using System.Linq;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using TokanPages.Middleware;
using TokanPages.Configuration;
using TokanPages.Backend.Shared.Settings;
using Serilog;

namespace TokanPages
{
    public class Startup
    {
        protected readonly IConfiguration FConfiguration;

        private readonly IWebHostEnvironment FEnvironment;

        public Startup(IConfiguration AConfiguration, IWebHostEnvironment AEnvironment)
        {
            FConfiguration = AConfiguration;
            FEnvironment = AEnvironment;
        }

        public virtual void ConfigureServices(IServiceCollection AServices)
        {
            AServices
                .AddMvc(AOption => AOption.CacheProfiles
                .Add("Standard", new CacheProfile
                { 
                    Duration = 10, 
                    Location = ResponseCacheLocation.Any, 
                    NoStore = false 
                }));

            AServices.AddControllers();        
            AServices.AddSpaStaticFiles(AOptions => AOptions.RootPath = "ClientApp/build");
            AServices.AddResponseCompression(AOptions => AOptions.Providers.Add<GzipCompressionProvider>());
            Dependencies.Register(AServices, FConfiguration);
            
            if (FEnvironment.IsDevelopment())
            {
                AServices.AddSwaggerGen(AOption =>
                    AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "TokanPagesApi", Version = "v1" }));
            }

            if (!FEnvironment.IsProduction() && !FEnvironment.IsStaging()) 
                return;
                
            // Since this app is meant to run in Docker only
            // We get the Docker's internal network IP(s)
            var LHostName = Dns.GetHostName();
            var LAddresses = Dns.GetHostEntry(LHostName).AddressList
                .Where(AIpAddress => AIpAddress.AddressFamily == AddressFamily.InterNetwork)
                .ToList();

            AServices.Configure<ForwardedHeadersOptions>(AOptions =>
            {
                AOptions.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                AOptions.ForwardLimit = null;
                AOptions.RequireHeaderSymmetry = false;

                foreach (var LAddress in LAddresses) 
                    AOptions.KnownProxies.Add(LAddress);
            });
        }

        public virtual void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            AApplication.UseSerilogRequestLogging();
            AApplication.UseForwardedHeaders();
            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
            AApplication.UseMiddleware<GarbageCollector>();
            AApplication.UseMiddleware<CustomCors>();

            AApplication.UseResponseCompression();
            AApplication.UseHttpsRedirection();
            AApplication.UseStaticFiles();
            AApplication.UseSpaStaticFiles();
            AApplication.UseRouting();

            if (FEnvironment.IsDevelopment())
            {
                AApplication.UseSwagger();
                AApplication.UseSwaggerUI(AOption =>
                    AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPagesApi version 1"));
            }

            AApplication.UseEndpoints(AEndpoints => 
                AEndpoints.MapControllers());

            AApplication.UseSpa(ASpa =>
            {
                ASpa.Options.SourcePath = "ClientApp";
                if (FEnvironment.IsDevelopment())
                    ASpa.UseProxyToSpaDevelopmentServer(AAppUrls.DevelopmentOrigin);
            });           
        }
    }
}
