using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Sender.Newsletters.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Sender.Newsletters;

public class AddSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenProvidedEmail_WhenAddNewsletter_ShouldAddEntity() 
    {
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
        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var existingNewsletter = new Newsletter
        {
            Email = testEmail,
            IsActivated = false,
            Count = 0,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = default,
            Id = Guid.NewGuid(),
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<string>()))
            .ReturnsAsync(existingNewsletter);

        var command = new AddNewsletterCommand { Email = testEmail };
        var handler = new AddNewsletterCommandHandler(
            mockedLogger.Object,
            mockSenderRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS));
    }
}