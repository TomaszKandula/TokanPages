using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using TokanPages.AppLogger;
using TokanPages.Middleware;
using TokanPages.Backend.Database;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.SmtpClient.Settings;
using TokanPages.Backend.Shared.Dto.Responses;
using TokanPages.Backend.Cqrs.Handlers.Commands;
using MediatR;

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

            AServices.AddSingleton<IAppLogger, AppLogger.AppLogger>();
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(Configuration.GetConnectionString("DbConnect"),
                AAddOptions => AAddOptions.EnableRetryOnFailure());
            });
            AServices.AddScoped<ISmtpClientService, SmtpClientService>();          
            AServices.AddScoped<IAzureStorageService, AzureStorageService>();

            AServices.AddMediatR(Assembly.GetExecutingAssembly());
            AServices.AddTransient<IRequestHandler<VerifyEmailAddressCommand, VerifyEmailAddressResponse>, VerifyEmailAddressCommandHandler>();

            AServices.AddResponseCompression(AOptions =>
            {
                AOptions.Providers.Add<GzipCompressionProvider>();
            });

            AServices.AddSwaggerGen(AOption =>
            {
                AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "TokanPagesApi", Version = "v1" });
            });

        }

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
                // See https://aka.ms/aspnetcore-hsts.
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
                AEndpoints.MapControllers();
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
