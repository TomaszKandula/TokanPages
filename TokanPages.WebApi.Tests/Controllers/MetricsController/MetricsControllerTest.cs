namespace TokanPages.WebApi.Tests.Controllers.MetricsController
{
    using Xunit;

    public partial class MetricsControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/metrics";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public MetricsControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
    }
}