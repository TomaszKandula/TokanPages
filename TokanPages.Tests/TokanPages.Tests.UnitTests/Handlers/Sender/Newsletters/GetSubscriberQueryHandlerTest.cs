using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Sender.Newsletters.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Sender.Newsletters;

public class GetSubscriberQueryHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenGetNewsletter_ShouldReturnEntity() 
    {
        // Arrange
        var newsletter = new Backend.Domain.Entities.Newsletter
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty,
            Id = Guid.NewGuid(),
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<Guid>()))
            .ReturnsAsync(newsletter);

        var query = new GetNewsletterQuery { Id = Guid.NewGuid() };
        var handler = new GetNewsletterQueryHandler(mockedLogger.Object, mockSenderRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(newsletter.Email);
        result.IsActivated.Should().BeTrue();
        result.NewsletterCount.Should().Be(newsletter.Count);
        result.CreatedAt.Should().Be(newsletter.CreatedAt);
        result.ModifiedAt.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetNewsletter_ShouldThrowError()
    {
        // Arrange
        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<string>()))
            .ReturnsAsync((Backend.Domain.Entities.Newsletter?)null);
        
        var query = new GetNewsletterQuery { Id = Guid.NewGuid() };
        var handler = new GetNewsletterQueryHandler(mockedLogger.Object, mockSenderRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS));
    }
}