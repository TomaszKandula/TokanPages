namespace TokanPages.IntegrationTests.MetricsController
{
    using Xunit;

    public partial class MetricsControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string ApiBaseUrl = "/api/v1/metrics";
        
        private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

        public MetricsControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
    }
}