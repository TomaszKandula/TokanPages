namespace TokanPages.IntegrationTests.UsersController
{
    using Xunit;

    public partial class UsersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string ApiBaseUrl = "/api/v1/users";
        
        private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

        public UsersControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
    }
}