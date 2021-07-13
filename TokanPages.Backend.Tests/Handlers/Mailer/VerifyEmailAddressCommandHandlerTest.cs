namespace TokanPages.Backend.Tests.Handlers.Mailer
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SmtpClient;
    using SmtpClient.Models;
    using Cqrs.Handlers.Commands.Mailer;
    using Shared.Services.DataProviderService;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class VerifyEmailAddressCommandHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public VerifyEmailAddressCommandHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenValidEmailAddress_WhenVerifyEmailAddress_ShouldFinishSuccessful()
        {
            // Arrange
            var LVerifyEmailAddressCommand = new VerifyEmailAddressCommand
            {
                Email = FDataProviderService.GetRandomEmail()
            };

            var LMockedSmtpClientService = new Mock<ISmtpClientService>();
            var LCheckActionResult = new List<EmailAddressModel>
            {
                new ()
                {
                    EmailAddress = LVerifyEmailAddressCommand.Email,
                    IsValid = true
                }
            };
            
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.IsAddressCorrect(It.IsAny<IEnumerable<string>>()))
                .Returns(LCheckActionResult);
            
            LMockedSmtpClientService
                .Setup(ASmtpClient => ASmtpClient.IsDomainCorrect(It.IsAny<string>(), CancellationToken.None))
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