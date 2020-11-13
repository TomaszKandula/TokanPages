using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TokanPages.BackEnd.Storage;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Logic.Mailer;
using TokanPages.BackEnd.Logic.MailChecker;

namespace BackEnd.UnitTests
{

    public class MailerTests
    {

        private readonly IMailer FMailer;
        private readonly IMailChecker FMailChecker;
        private readonly AzureStorage FAzureStorage;

        public MailerTests() 
        {

            // To be removed when mocks are provided
            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<MailerTests>()
                .Build();

            var FSendGridKeys = Configuration.GetSection("SendGridKeys").Get<SendGridKeys>();
            var FAzureStorage = Configuration.GetSection("AzureStorage").Get<AzureStorage>();

            var LAzureStorageService = new AzureStorageService(FAzureStorage);

            FMailer = new Mailer(FSendGridKeys, LAzureStorageService);
            FMailChecker = new MailChecker();

        }

        [Fact]
        public void FieldsCheck_Test()
        {

            // Arrange
            FMailer.From    = "";
            FMailer.To      = " ";
            FMailer.Subject = "First email";
            FMailer.Body    = "Hello World!";

            // Act
            var LResult = FMailer.FieldsCheck();

            // Assert
            LResult.Should().BeFalse();

        }

        [Fact]
        public void IsAddressCorrect_Test() 
        {

            // Arrange
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
            var LTestEmail = "john@fakedomain.oi";

            // Act
            var LResult = await FMailChecker.IsDomainCorrect(LTestEmail);

            // Assert
            LResult.Should().Be(false);
        
        }

        [Fact]
        public async Task Send_Test() 
        {

            // Arrange
            var NewGuid = Guid.NewGuid();
            FMailer.From    = "contact@tomkandula.com";
            FMailer.To      = "tomasz.kandula@gmail.com";
            FMailer.Subject = "Test email";
            FMailer.Body    = $"<p>Hello World!</p><p>Test run: {NewGuid}</p>";

            // Act
            var LResult = await FMailer.Send();

            // Assert
            LResult.ErrorMessage.Should().Be("n/a");
            LResult.IsSucceeded.Should().BeTrue();
        
        }

    }

}
