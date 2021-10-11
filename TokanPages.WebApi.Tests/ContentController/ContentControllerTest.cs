namespace TokanPages.WebApi.Tests.ContentController
{
    using Xunit;

    public partial class ContentControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/content";

        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public ContentControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
    }
}