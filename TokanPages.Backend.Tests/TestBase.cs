namespace TokanPages.Backend.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Database;
    using Identity.Services.JwtUtilityService;
    using Shared.Services.DataProviderService;

    public class TestBase
    {
        protected IDataProviderService DataProviderService { get; }
        
        protected IJwtUtilityService JwtUtilityService { get; }

        private readonly DatabaseContextFactory FDatabaseContextFactory;
        
        protected TestBase()
        {
            DataProviderService = new DataProviderService();
            JwtUtilityService = new JwtUtilityService();

            var LServices = new ServiceCollection();
            LServices.AddSingleton<DatabaseContextFactory>();
            LServices.AddScoped(AContext =>
            {
                var LFactory = AContext.GetService<DatabaseContextFactory>();
                return LFactory?.CreateDatabaseContext();
            });

            var LServiceScope = LServices.BuildServiceProvider(true).CreateScope();
            var LServiceProvider = LServiceScope.ServiceProvider;
            FDatabaseContextFactory = LServiceProvider.GetService<DatabaseContextFactory>();
        }

        protected DatabaseContext GetTestDatabaseContext() =>  FDatabaseContextFactory.CreateDatabaseContext();
    }
}