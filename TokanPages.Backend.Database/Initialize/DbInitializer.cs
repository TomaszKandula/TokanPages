using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Database.Seeders;

namespace TokanPages.Backend.Database.Initialize
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory FScopeFactory;

        public DbInitializer(IServiceScopeFactory AScopeFactory)
            => FScopeFactory = AScopeFactory;

        public void StartMigration()
        {
            using var LServiceScope = FScopeFactory.CreateScope();
            using var LDatabaseContext = LServiceScope.ServiceProvider.GetService<DatabaseContext>();

            if (LDatabaseContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory") 
                LDatabaseContext.Database.Migrate();
        }

        public async Task SeedData()
        {
            using var LServiceScope = FScopeFactory.CreateScope();
            await using var LDatabaseContext = LServiceScope.ServiceProvider.GetService<DatabaseContext>();

            if (!LDatabaseContext.Users.AnyAsync().GetAwaiter().GetResult())
                LDatabaseContext.Users.AddRange(UsersSeeder.SeedUsers());

            if (!LDatabaseContext.Articles.AnyAsync().GetAwaiter().GetResult())
                LDatabaseContext.Articles.AddRange(ArticlesSeeder.SeedArticles());

            if (!LDatabaseContext.Subscribers.AnyAsync().GetAwaiter().GetResult())
                LDatabaseContext.Subscribers.AddRange(SubscribersSeeder.SeedSubscribers());

            if (!LDatabaseContext.ArticleLikes.AnyAsync().GetAwaiter().GetResult())
                LDatabaseContext.ArticleLikes.AddRange(ArticleLikesSeeder.SeedArticleLikes());
            
            if (!LDatabaseContext.PhotoCategories.AnyAsync().GetAwaiter().GetResult())
                LDatabaseContext.PhotoCategories.AddRange(PhotoCategoriesSeeder.SeedPhotoCategories());

            await LDatabaseContext.SaveChangesAsync();
        }
    }
}
