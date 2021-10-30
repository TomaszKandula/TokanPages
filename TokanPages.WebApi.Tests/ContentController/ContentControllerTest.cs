namespace TokanPages.WebApi.Tests.ContentController
{
    using Xunit;

    public partial class ContentControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string ApiBaseUrl = "/api/v1/content";

        private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

        public ContentControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
    }
}