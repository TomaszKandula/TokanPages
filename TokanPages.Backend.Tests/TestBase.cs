namespace TokanPages.Backend.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Database;
    using Shared.Services.DateTimeService;
    using Shared.Services.DataUtilityService;
    using Identity.Services.JwtUtilityService;

    public class TestBase
    {
        protected IDataUtilityService DataUtilityService { get; }
        
        protected IJwtUtilityService JwtUtilityService { get; }

        protected IDateTimeService DateTimeService { get; }
        
        private readonly DatabaseContextFactory FDatabaseContextFactory;
        
        protected TestBase()
        {
            DataUtilityService = new DataUtilityService();
            JwtUtilityService = new JwtUtilityService();
            DateTimeService = new DateTimeService();

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