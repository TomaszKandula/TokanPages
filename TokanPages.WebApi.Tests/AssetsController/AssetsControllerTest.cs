namespace TokanPages.WebApi.Tests.AssetsController
{
    using Xunit;

    public partial class AssetsControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string ApiBaseUrl = "/api/v1/assets";
        
        private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

        public AssetsControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
    }
}