using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Helpers;
using TokanPages.Persistence.DataAccess.Repositories.Articles;

namespace TokanPages.Persistence.DataAccess;

public static class ConfigurationExtension
{
    public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetupDatabase<OperationDbContext>(configuration);//TODO: to be removed

        services.AddScoped<ISqlGenerator, SqlGenerator>();
        services.AddScoped<IDbOperations, DbOperations>();
        services.AddScoped<IArticlesRepository, ArticlesRepository>();
    }

    private static void SetupDatabase<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        const int maxRetryCount = 10;
        var settings = BoundAppSettings.GetSettings(configuration);
        var maxRetryDelay = TimeSpan.FromSeconds(5);
        var connectionString = settings.DbDatabaseContext;

        services.AddDbContext<T>(options =>
        {
            options.UseSqlServer(connectionString, addOptions 
                => addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
        });
    }
}