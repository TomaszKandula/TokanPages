namespace TokanPages.WebApi.Tests.Controllers.MailerController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Shared.Dto.Mailer;
    using Backend.Cqrs.Handlers.Commands.Mailer;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public partial class MailerControllerTest
    {
        [Theory]
        [InlineData("john@gmail.com")]
        public async Task GivenValidEmailAndValidJwt_WhenVerifyEmailAddress_ShouldReturnResultsAsJsonObject(string AEmail)
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/VerifyEmailAddress/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            var LPayLoad = new VerifyEmailAddressDto { Email = AEmail };

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<VerifyEmailAddressCommandResult>(LContent);
            LDeserialized?.IsFormatCorrect.Should().BeTrue();
            LDeserialized?.IsDomainCorrect.Should().BeTrue();
        }
    }
}