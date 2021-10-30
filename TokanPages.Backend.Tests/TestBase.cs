namespace TokanPages.Backend.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Database;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.DataUtilityService;
    using Identity.Services.JwtUtilityService;

    public class TestBase
    {
        protected IDataUtilityService DataUtilityService { get; }
        
        protected IJwtUtilityService JwtUtilityService { get; }

        protected IDateTimeService DateTimeService { get; }
        
        private readonly DatabaseContextFactory _databaseContextFactory;
        
        protected TestBase()
        {
            DataUtilityService = new DataUtilityService();
            JwtUtilityService = new JwtUtilityService();
            DateTimeService = new DateTimeService();

            var services = new ServiceCollection();
            services.AddSingleton<DatabaseContextFactory>();
            services.AddScoped(context =>
            {
                var factory = context.GetService<DatabaseContextFactory>();
                return factory?.CreateDatabaseContext();
            });

            var serviceScope = services.BuildServiceProvider(true).CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            _databaseContextFactory = serviceProvider.GetService<DatabaseContextFactory>();
        }

        protected DatabaseContext GetTestDatabaseContext() =>  _databaseContextFactory.CreateDatabaseContext();
    }
}