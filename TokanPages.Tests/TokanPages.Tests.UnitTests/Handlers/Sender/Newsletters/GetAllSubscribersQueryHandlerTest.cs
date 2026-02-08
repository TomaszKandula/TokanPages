using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Sender.Newsletters.Queries;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Sender.Newsletters;

public class GetAllSubscribersQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllNewsletters_ShouldReturnCollection()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        var newsletters = new List<Backend.Domain.Entities.Newsletter>
        {
            new()
            {
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                CreatedAt = DataUtilityService.GetRandomDateTime(),
                CreatedBy = Guid.Empty,
                Id = Guid.NewGuid(),
            },
            new()
            {
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 100,
                CreatedAt = DataUtilityService.GetRandomDateTime(),
                CreatedBy = Guid.Empty,
                Id = Guid.NewGuid(),
            }
        };

        mockSenderRepository
            .Setup(repository => repository.GetNewsletters(It.IsAny<bool>()))
            .ReturnsAsync(newsletters);

        var query = new GetNewslettersQuery();
        var handler = new GetNewslettersQueryHandler(databaseContext, mockedLogger.Object, mockSenderRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ToList().Should().NotBeNull();
        result.ToList().Should().HaveCount(2);
    }
}