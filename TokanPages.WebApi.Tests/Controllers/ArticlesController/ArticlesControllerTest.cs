namespace TokanPages.WebApi.Tests.Controllers.ArticlesController
{
    using Xunit;

    public partial class ArticlesControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/articles";

        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public ArticlesControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
    }
}