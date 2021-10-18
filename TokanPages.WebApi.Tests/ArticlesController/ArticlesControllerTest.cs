namespace TokanPages.WebApi.Tests.ArticlesController
{
    using Xunit;

    public partial class ArticlesControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string ApiBaseUrl = "/api/v1/articles";

        private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

        public ArticlesControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
    }
}