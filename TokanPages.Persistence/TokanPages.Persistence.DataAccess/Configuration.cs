using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Chat;
using TokanPages.Persistence.DataAccess.Repositories.Content;
using TokanPages.Persistence.DataAccess.Repositories.Invoicing;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Persistence.DataAccess.Repositories.Notification;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using TokanPages.Persistence.DataAccess.Repositories.User;

namespace TokanPages.Persistence.DataAccess;

public static class Configuration
{
    public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetupDatabase<OperationDbContext>(configuration);//TODO: to be removed

        services.AddScoped<ISqlGenerator, SqlGenerator>();
        services.AddScoped<IDbOperations, DbOperations>();

        services.AddScoped<IArticlesRepository, ArticlesRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IMessagingRepository, MessagingRepository>();
        services.AddScoped<IInvoicingRepository, InvoicingRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRevenueRepository, RevenueRepository>();
        services.AddScoped<ISenderRepository, SenderRepository>();
    }

    private static void SetupDatabase<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        const int maxRetryCount = 10;
        var settings = configuration.GetAppSettings();
        var maxRetryDelay = TimeSpan.FromSeconds(5);
        var connectionString = settings.DbDatabaseContext;

        services.AddDbContext<T>(options =>
        {
            options.UseSqlServer(connectionString, addOptions 
                => addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
        });
    }
}