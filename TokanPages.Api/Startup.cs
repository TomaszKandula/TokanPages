using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using TokanPages.Api.Middleware;
using TokanPages.Api.Configuration;
using TokanPages.Backend.Shared.Settings;
using Serilog;

namespace TokanPages.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IConfiguration FConfiguration;

        private readonly IWebHostEnvironment FEnvironment;

        public Startup(IConfiguration AConfiguration, IWebHostEnvironment AEnvironment)
        {
            FConfiguration = AConfiguration;
            FEnvironment = AEnvironment;
        }

        public void ConfigureServices(IServiceCollection AServices)
        {
            AServices.AddControllers();        
            AServices.AddSpaStaticFiles(AOptions => AOptions.RootPath = "ClientApp/build");
            AServices.AddResponseCompression(AOptions => AOptions.Providers.Add<GzipCompressionProvider>());
            Dependencies.Register(AServices, FConfiguration, FEnvironment);
            
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

        public void Configure(IApplicationBuilder AApplication, AppUrls AAppUrls)
        {
            AApplication.UseSerilogRequestLogging();

            AApplication.UseMiddleware<CustomCors>();
            AApplication.UseMiddleware<CustomException>();
            
            AApplication.UseHttpsRedirection();
            AApplication.UseForwardedHeaders();
            AApplication.UseResponseCompression();

            AApplication.UseStaticFiles();
            AApplication.UseSpaStaticFiles();
            AApplication.UseRouting();
            AApplication.UseEndpoints(AEndpoints => AEndpoints.MapControllers());

            if (FEnvironment.IsDevelopment())
            {
                AApplication.UseSwagger();
                AApplication.UseSwaggerUI(AOption =>
                    AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "TokanPagesApi version 1"));
            }

            AApplication.UseSpa(ASpa =>
            {
                ASpa.Options.SourcePath = "ClientApp";
                if (FEnvironment.IsDevelopment())
                    ASpa.UseProxyToSpaDevelopmentServer(AAppUrls.DevelopmentOrigin);
            });           
        }
    }
}
