using System.Reflection;
using System.Collections.Generic;
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
using TokanPages.CustomHandlers;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Settings;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.SmtpClient.Settings;
using TokanPages.Backend.Core.TemplateHelper;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;
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
            AServices.AddSingleton(Configuration.GetSection("AppUrls").Get<AppUrls>());

            AServices.AddSingleton<IAppLogger, AppLogger.AppLogger>();
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(Configuration.GetConnectionString("DbConnect"),
                AAddOptions => AAddOptions.EnableRetryOnFailure());
            });
            AServices.AddScoped<ISmtpClientService, SmtpClientService>();          
            AServices.AddScoped<IAzureStorageService, AzureStorageService>();
            AServices.AddScoped<ITemplateHelper, TemplateHelper>();

            AServices.AddMediatR(Assembly.GetExecutingAssembly());
            AServices.AddTransient<IRequestHandler<VerifyEmailAddressCommand, VerifyEmailAddressCommandResult>, VerifyEmailAddressCommandHandler>();
            AServices.AddTransient<IRequestHandler<SendMessageCommand, Unit>, SendMessageCommandHandler>();
            AServices.AddTransient<IRequestHandler<SendNewsletterCommand, Unit>, SendNewsletterCommandHandler>();
            AServices.AddTransient<IRequestHandler<GetAllArticlesQuery, IEnumerable<Backend.Domain.Entities.Articles>>, GetAllArticlesQueryHandler>();
            AServices.AddTransient<IRequestHandler<GetArticleQuery, Backend.Domain.Entities.Articles>, GetArticleQueryHandler>();
            AServices.AddTransient<IRequestHandler<AddArticleCommand, Unit>, AddArticleCommandHandler>();
            AServices.AddTransient<IRequestHandler<UpdateArticleCommand, Unit>, UpdateArticleCommandHandler>();
            AServices.AddTransient<IRequestHandler<RemoveArticleCommand, Unit>, RemoveArticleCommandHandler>();
            AServices.AddTransient<IRequestHandler<GetAllSubscribersQuery, IEnumerable<Backend.Domain.Entities.Subscribers>>, GetAllSubscribersQueryHandler>();
            AServices.AddTransient<IRequestHandler<GetSubscriberQuery, Backend.Domain.Entities.Subscribers>, GetSubscriberQueryHandler>();
            AServices.AddTransient<IRequestHandler<AddSubscriberCommand, Unit>, AddSubscriberCommandHandler>();
            AServices.AddTransient<IRequestHandler<UpdateSubscriberCommand, Unit>, UpdateSubscriberCommandHandler>();
            AServices.AddTransient<IRequestHandler<RemoveSubscriberCommand, Unit>, RemoveSubscriberCommandHandler>();

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

            AApplication.UseResponseCompression();
            AApplication.UseMiddleware<GarbageCollector>();
            AApplication.UseMiddleware<CustomCors>();

            if (AEnvironment.IsDevelopment())
                AApplication.UseDeveloperExceptionPage();

            AApplication.UseExceptionHandler(ExceptionHandler.Handle);
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
