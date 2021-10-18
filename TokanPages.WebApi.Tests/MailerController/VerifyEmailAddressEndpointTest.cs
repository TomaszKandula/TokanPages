namespace TokanPages.WebApi.Tests.MailerController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Shared.Dto.Mailer;
    using Backend.Cqrs.Handlers.Commands.Mailer;

    public partial class MailerControllerTest
    {
        [Theory]
        [InlineData("john@gmail.com")]
        public async Task GivenValidEmailAndValidJwt_WhenVerifyEmailAddress_ShouldReturnResultsAsJsonObject(string email)
        {
            // Arrange
            var request = $"{ApiBaseUrl}/VerifyEmailAddress/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);
            var payLoad = new VerifyEmailAddressDto { Email = email };

            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();

            var deserialized = JsonConvert.DeserializeObject<VerifyEmailAddressCommandResult>(content);
            deserialized?.IsFormatCorrect.Should().BeTrue();
            deserialized?.IsDomainCorrect.Should().BeTrue();
        }
    }
}