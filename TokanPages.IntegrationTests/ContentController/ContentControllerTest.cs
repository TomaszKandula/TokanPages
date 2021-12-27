namespace TokanPages.IntegrationTests.ContentController;

using Xunit;

public partial class ContentControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/content";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public ContentControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
}