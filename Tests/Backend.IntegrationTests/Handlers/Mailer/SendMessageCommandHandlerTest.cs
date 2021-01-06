﻿using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.TestData;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Mailer;

namespace Backend.IntegrationTests.Handlers.Mailer
{

    public class SendMessageCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public SendMessageCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task SendUserMessage_WhenEmailIsProvided_ShouldReturnEmptyJsonObject()
        {

            // Arrange
            var LRequest = "/api/v1/mailer/sendmessage/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new SendMessageDto
            {
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                UserEmail = DataProvider.GetRandomEmail(),
                EmailFrom = DataProvider.GetRandomEmail(),
                EmailTos = new List<string> { string.Empty },
                Subject = "Integration Test / HttpClient / Endpoint",
                Message = $"Test run Id: {Guid.NewGuid()}.",
            };

            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("{}");

        }

    }

}
