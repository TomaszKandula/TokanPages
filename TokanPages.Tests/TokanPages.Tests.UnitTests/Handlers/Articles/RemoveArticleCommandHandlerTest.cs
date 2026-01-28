using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
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
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

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
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(true);

        var command = new RemoveArticleCommand { Id = articleId };
        var handler = new RemoveArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedArticlesRepository.Object);

        // Act 
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenIncorrectId_WhenRemoveArticle_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(Guid.NewGuid());

        mockedArticlesRepository
            .Setup(repository => repository.RemoveArticle(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(false);

        var command = new RemoveArticleCommand { Id = Guid.NewGuid() };
        var handler = new RemoveArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedArticlesRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}