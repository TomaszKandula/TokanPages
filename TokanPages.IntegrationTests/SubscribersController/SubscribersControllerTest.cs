namespace TokanPages.IntegrationTests.SubscribersController;

using Xunit;

public partial class SubscribersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/subscribers";
        
    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public SubscribersControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
}