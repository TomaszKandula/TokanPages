using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Sender.Newsletters.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Sender.Newsletters;

public class AddSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenProvidedEmail_WhenAddNewsletter_ShouldAddEntity() 
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<string>()))
            .ReturnsAsync((Newsletter?)null);

        var command = new AddNewsletterCommand
        {
            Email = DataUtilityService.GetRandomEmail()
        };

        var handler = new AddNewsletterCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockSenderRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GivenExistingEmail_WhenAddNewsletter_ShouldThrowError()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var existingNewsletter = new Newsletter { Email = testEmail };

        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<string>()))
            .ReturnsAsync(existingNewsletter);

        var command = new AddNewsletterCommand { Email = testEmail };
        var handler = new AddNewsletterCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockSenderRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS));
    }
}