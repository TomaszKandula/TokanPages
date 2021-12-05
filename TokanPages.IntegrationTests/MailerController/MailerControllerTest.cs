namespace TokanPages.IntegrationTests.MailerController
{
    using Xunit;

    public partial class MailerControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string ApiBaseUrl = "/api/v1.0/Mailer";
        
        private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;
        
        public MailerControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;
    }
}