using System;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Backend.Database;

namespace Backend.UnitTests
{
    public class TestBase
    {
        protected IServiceProvider FServiceProvider;
        protected IServiceScope FServiceScope;
        private readonly DatabaseContextFactory FDatabaseContextFactory;

        public TestBase() 
        {
            var LServices = new ServiceCollection();

            LServices.AddSingleton<DatabaseContextFactory>();
            LServices.AddScoped(AContext =>
            {
                var LFactory = AContext.GetService<DatabaseContextFactory>();
                return LFactory.CreateDatabaseContext();
            });

            var LServiceProvider = LServices.BuildServiceProvider(true);
            FServiceScope = LServiceProvider.CreateScope();
            FServiceProvider = FServiceScope.ServiceProvider;
            FDatabaseContextFactory = FServiceProvider.GetService<DatabaseContextFactory>();
        }

        protected DatabaseContext GetTestDatabaseContext()
        {
            return FDatabaseContextFactory.CreateDatabaseContext();
        }
    }
}
