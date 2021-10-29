namespace TokanPages.Backend.Tests.Handlers.Mailer
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SmtpClient;
    using Core.Utilities.LoggerService;
    using SmtpClient.Models;
    using Cqrs.Handlers.Commands.Mailer;

    public class VerifyEmailAddressCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidEmailAddress_WhenVerifyEmailAddress_ShouldFinishSuccessful()
        {
            // Arrange
            var verifyEmailAddressCommand = new VerifyEmailAddressCommand
            {
                Email = DataUtilityService.GetRandomEmail()
            };

            var databaseContext = GetTestDatabaseContext();
            var mockedLogger = new Mock<ILoggerService>();
            var mockedSmtpClientService = new Mock<ISmtpClientService>();

            var checkActionResult = new List<Email>
            {
                new ()
                {
                    Address = verifyEmailAddressCommand.Email,
                    IsValid = true
                }
            };
            
            mockedSmtpClientService
                .Setup(client => client.IsAddressCorrect(It.IsAny<IEnumerable<string>>()))
                .Returns(checkActionResult);
            
            mockedSmtpClientService
                .Setup(client => client.IsDomainCorrect(It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(true));
            
            var verifyEmailAddressCommandHandler = new VerifyEmailAddressCommandHandler(
                databaseContext,
                mockedLogger.Object,
                mockedSmtpClientService.Object);

            // Act
            var result = await verifyEmailAddressCommandHandler.Handle(verifyEmailAddressCommand, CancellationToken.None);

            // Assert
            result.IsDomainCorrect.Should().Be(true);
            result.IsFormatCorrect.Should().Be(true);
        }
    }
}