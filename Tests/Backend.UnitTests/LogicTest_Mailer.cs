using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Logic.Mailer;
using BackEnd.UnitTests.FakeSendGrid;
using BackEnd.UnitTests.FakeAzureStorage;
using TokanPages.BackEnd.Logic.Mailer.Model;

namespace BackEnd.UnitTests
{

    public class LogicTest_Mailer
    {

        [Fact]
        public void Should_InvalidateInputs()
        {

            // Arrange
            var FakeSmtpClientService = new SmtpClientService();
            var FakeAzureStorageService = new AzureStorageService();
            var FMailer = new Mailer(FakeSmtpClientService, FakeAzureStorageService);

            FMailer.From    = "";
            FMailer.Tos     = new List<string> { " " };
            FMailer.Subject = "First email";
            FMailer.Body    = "Hello World!";

            // Act
            var LResult = FMailer.ValidateInputs();

            // Assert
            LResult.Should().BeFalse();

        }

        [Fact]
        public async Task Should_MakeBody() 
        {

            // Arrange
            var FakeSmtpClientService = new SmtpClientService();
            var FakeAzureStorageService = new AzureStorageService();
            var FMailer = new Mailer(FakeSmtpClientService, FakeAzureStorageService);

            var LTestTemplate = "This is {VAL1} string to {VAL2} some method...";

            var LValueTag = new List<ValueTag> 
            { 
                new ValueTag{ Tag = "{VAL1}", Value = "test" },
                new ValueTag{ Tag = "{VAL2}", Value = "examine" }
            };

            // Act
            var LResult = await FMailer.MakeBody(LTestTemplate, LValueTag);

            // Assert
            LResult.Should().Be("This is test string to examine some method...");

        }

        [Fact]
        public async Task Should_SendEmail() 
        {

            // Arrange
            var FakeSmtpClientService = new SmtpClientService();
            var FakeAzureStorageService = new AzureStorageService();
            var FMailer = new Mailer(FakeSmtpClientService, FakeAzureStorageService);

            FMailer.From    = "contact@tomkandula.com";
            FMailer.Tos     = new List<string> { "tomasz.kandula@gmail.com" };
            FMailer.Subject = "Test email";
            FMailer.Body    = "<p>Hello World!</p>";

            // Act
            var LResult = await FMailer.Send();

            // Assert
            LResult.ErrorDesc.Should().Be("n/a");
            LResult.IsSucceeded.Should().BeTrue();
        
        }

    }

}
