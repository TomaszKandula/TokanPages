using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Chat;

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