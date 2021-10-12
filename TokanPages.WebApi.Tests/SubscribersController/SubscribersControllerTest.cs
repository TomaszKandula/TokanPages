namespace TokanPages.WebApi.Tests.SubscribersController
{
    using Xunit;

    public partial class SubscribersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/subscribers";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public SubscribersControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
    }
}