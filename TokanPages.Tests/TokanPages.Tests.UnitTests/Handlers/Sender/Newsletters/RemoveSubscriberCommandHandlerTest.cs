using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Sender.Newsletters.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Sender.Newsletters;

public class RemoveSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveNewsletter_ShouldRemoveEntity() 
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var newsletter = new Newsletter 
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<Guid>()))
            .ReturnsAsync(newsletter);
        
        var command = new RemoveNewsletterCommand { Id = newsletter.Id };
        var handler = new RemoveNewsletterCommandHandler(databaseContext, mockedLogger.Object, mockSenderRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Unit>();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenRemoveNewsletter_ShouldThrowError()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var command = new RemoveNewsletterCommand { Id = Guid.NewGuid() };
        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<Guid>()))
            .ReturnsAsync((Newsletter?)null);

        var handler = new RemoveNewsletterCommandHandler(databaseContext, mockedLogger.Object, mockSenderRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS));        
    }
}