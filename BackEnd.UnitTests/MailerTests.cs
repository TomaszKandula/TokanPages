using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TokanPages.BackEnd.Mailer;
using TokanPages.BackEnd.Settings;

namespace BackEnd.UnitTests
{

    public class MailerTests
    {

        private readonly IMailer FMailer;

        public MailerTests() 
        {

            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<MailerTests>()
                .Build();

            var FSendGridKeys = Configuration.GetSection("SendGridKeys").Get<SendGridKeys>();

            FMailer = new Mailer(FSendGridKeys);

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
        public void CheckEmailAddresses_Test() 
        {

            // Arrange
            var Emails = new List<string>() 
            { 
                "this is not an email",
                "tom@tomkandula.com",
                "contact@tomkandula.com",
                "tomasz.kandula@gmail.com"
            };

            // Act
            var LResults = FMailer.CheckEmailAddresses(Emails);

            // Assert
            LResults.Should().HaveCount(4);
            LResults.Select(Addresses => Addresses.IsValid).Contains(false).Should().BeTrue();

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
