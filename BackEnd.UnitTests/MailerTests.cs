using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Logic.Mailer;
using TokanPages.BackEnd.Logic.MailChecker;
using BackEnd.UnitTests.Mocks.SendGrid;
using BackEnd.UnitTests.Mocks.AzureStorage;
using TokanPages.BackEnd.Logic.Mailer.Model;

namespace BackEnd.UnitTests
{

    public class MailerTests
    {

        [Fact]
        public void ValidateInputs_Test()
        {

            // Arrange
            var FakeSendGridService = new FakeSendGridService();
            var FakeAzureStorageService = new FakeAzureStorageService();
            var FMailer = new Mailer(FakeSendGridService, FakeAzureStorageService);

            FMailer.From    = "";
            FMailer.To      = " ";
            FMailer.Subject = "First email";
            FMailer.Body    = "Hello World!";

            // Act
            var LResult = FMailer.ValidateInputs();

            // Assert
            LResult.Should().BeFalse();

        }

        [Fact]
        public void IsAddressCorrect_Test() 
        {

            // Arrange
            var FakeSendGridService = new FakeSendGridService();
            var FakeAzureStorageService = new FakeAzureStorageService();
            var FMailer = new Mailer(FakeSendGridService, FakeAzureStorageService);
            var FMailChecker = new MailChecker();

            var LTestEmails = new List<string>() 
            { 
                "this is not an email",
                "tom@tomkandula.com",
                "contact@tomkandula.com",
                "tomasz.kandula@gmail.com"
            };

            // Act
            var LResults = FMailChecker.IsAddressCorrect(LTestEmails);

            // Assert
            LResults.Should().HaveCount(4);
            LResults.Select(Addresses => Addresses.IsValid).Contains(false).Should().BeTrue();

        }

        [Fact]
        public async Task IsDomainCorrect_Test() 
        {

            // Arrange
            var FMailChecker = new MailChecker();
            var LTestEmail = "john@fakedomain.oi";

            // Act
            var LResult = await FMailChecker.IsDomainCorrect(LTestEmail);

            // Assert
            LResult.Should().Be(false);
        
        }

        [Fact]
        public async Task MakeBody_Test() 
        {

            // Arrange
            var FakeSendGridService = new FakeSendGridService();
            var FakeAzureStorageService = new FakeAzureStorageService();
            var FMailer = new Mailer(FakeSendGridService, FakeAzureStorageService);

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
        public async Task Send_Test() 
        {

            // Arrange
            var FakeSendGridService = new FakeSendGridService();
            var FakeAzureStorageService = new FakeAzureStorageService();
            var FMailer = new Mailer(FakeSendGridService, FakeAzureStorageService);

            FMailer.From    = "contact@tomkandula.com";
            FMailer.To      = "tomasz.kandula@gmail.com";
            FMailer.Subject = "Test email";
            FMailer.Body    = "<p>Hello World!</p>";

            // Act
            var LResult = await FMailer.Send();

            // Assert
            LResult.ErrorMessage.Should().Be("n/a");
            LResult.IsSucceeded.Should().BeTrue();
        
        }

    }

}
