using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class RemoveArticleCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveArticle_ShouldRemoveEntity() 
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedArticlesRepository
            .Setup(repository => repository.RemoveArticle(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>())
            )
            .Returns(Task.CompletedTask);

        var command = new RemoveArticleCommand { Id = articleId };
        var handler = new RemoveArticleCommandHandler(
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedArticlesRepository.Object);

        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }
}