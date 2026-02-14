using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class UpdateArticleCountCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenAnonymousUserAndExistingArticleAndNoReads_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();

        var mockedIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        var articleCounts = new List<ArticleCount>
        { 
            new()
            {
                Id = articleId,
                ReadCount = 2048,
                ArticleId = Guid.NewGuid(),
                IpAddress = string.Empty,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = default
            }
        };

        mockedArticlesRepository
            .Setup(repository => repository.GetArticleCount(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(articleCounts);

        mockedArticlesRepository
            .Setup(repository => repository.UpdateArticleCount(
            It.IsAny<Guid>(),
            It.IsAny<Guid>(),
            It.IsAny<int>(),
            It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(mockedIpAddress);

        var handler = new UpdateArticleCountCommandHandler(
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedArticlesRepository.Object);

        var command = new UpdateArticleCountCommand { Id = articleId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }
}