using Xunit;
using Moq;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Generators;
using TokanPages.Backend.SmtpClient.Models;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

namespace TokanPages.Backend.Tests.Handlers.Mailer
{
    public class VerifyEmailAddressCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidEmailAddress_WhenVerifyEmailAddress_ShouldFinishSuccessful()
        {
            // Arrange
            var LVerifyEmailAddressCommand = new VerifyEmailAddressCommand
            {
                Email = StringProvider.GetRandomEmail()
            };

            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LCheckActionResult = new List<EmailAddressModel>
            {
                new EmailAddressModel
                {
                    EmailAddress = LVerifyEmailAddressCommand.Email,
                    IsValid = true
                }
            };
            
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.IsAddressCorrect(It.IsAny<IEnumerable<string>>()))
                .Returns(LCheckActionResult);
            
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.IsDomainCorrect(It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            
            var LVerifyEmailAddressCommandHandler = new VerifyEmailAddressCommandHandler(LMockedSmtpClientService.Object);

            // Act
            var LResult = await LVerifyEmailAddressCommandHandler.Handle(LVerifyEmailAddressCommand, CancellationToken.None);

            // Assert
            LResult.IsDomainCorrect.Should().Be(true);
            LResult.IsFormatCorrect.Should().Be(true);
        }
    }
}