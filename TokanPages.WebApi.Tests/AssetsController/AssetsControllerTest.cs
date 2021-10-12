namespace TokanPages.WebApi.Tests.AssetsController
{
    using Xunit;

    public partial class AssetsControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/assets";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public AssetsControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
    }
}