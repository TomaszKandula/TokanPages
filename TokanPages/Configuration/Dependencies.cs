using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.AppLogger;
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

namespace TokanPages.Configuration
{

    public static class Dependencies
    {

        public static void Register(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            SetupAppSettings(AServices, AConfiguration);
            SetupLogger(AServices);
            SetupDatabase(AServices, AConfiguration);
            SetupServices(AServices);
            SetupMediatR(AServices);
        }

        private static void SetupAppSettings(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddSingleton(AConfiguration.GetSection("AzureStorage").Get<AzureStorageSettings>());
            AServices.AddSingleton(AConfiguration.GetSection("SmtpServer").Get<SmtpServerSettings>());
            AServices.AddSingleton(AConfiguration.GetSection("AppUrls").Get<AppUrls>());
        }

        private static void SetupLogger(IServiceCollection AServices) 
        {
            AServices.AddSingleton<IAppLogger, AppLogger.AppLogger>();
        }

        private static void SetupDatabase(IServiceCollection AServices, IConfiguration AConfiguration) 
        {
            AServices.AddDbContext<DatabaseContext>(AOptions =>
            {
                AOptions.UseSqlServer(AConfiguration.GetConnectionString("DbConnect"),
                AAddOptions => AAddOptions.EnableRetryOnFailure());
            });
        }

        private static void SetupServices(IServiceCollection AServices) 
        {
            AServices.AddScoped<ISmtpClientService, SmtpClientService>();
            AServices.AddScoped<IAzureStorageService, AzureStorageService>();
            AServices.AddScoped<ITemplateHelper, TemplateHelper>();
        }

        private static void SetupMediatR(IServiceCollection AServices) 
        {
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
        }

    }

}
