using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Persistence.DataAccess;

public static class ConfigurationExtension
{
    public static void SetupDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetupDatabase<OperationDbContext>(configuration);
        services.SetupDatabase<SoccerDbContext>(configuration);
    }

    private static void SetupDatabase<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        const int maxRetryCount = 10;
        var maxRetryDelay = TimeSpan.FromSeconds(5);
        var connectionString = configuration.GetValue<string>("Db_DatabaseContext") ?? "";

        services.AddDbContext<T>(options =>
        {
            options.UseSqlServer(connectionString, addOptions 
                => addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
        });
    }
}