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

public class UpdateArticleLikesCommandHandlerTest : TestBase
{
    private const string IpAddress = "255.255.255.255";

    [Fact]
    public async Task GivenNewLikes_WhenUpdateArticleLikes_ShouldSucceed()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();
        var mockedConfiguration = GetMockSettings();

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(IpAddress);

        mockedArticlesRepository
            .Setup(repository => repository.UpdateArticleLikes(
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<string>()
                ))
            .Returns(Task.CompletedTask);

        var articleLikes = new ArticleLike
        {
            LikeCount = 10,
            ArticleId = Guid.NewGuid(),
            IpAddress = string.Empty,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = default,
            Id = Guid.NewGuid(),
        };

        mockedArticlesRepository
            .Setup(repository => repository.GetArticleLikes(
                It.IsAny<bool>(), 
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                It.IsAny<string>()
                ))
            .ReturnsAsync(articleLikes);

        var command = new UpdateArticleLikesCommand
        {
            Id = articleId,
            AddToLikes = 10,
        };

        var handler = new UpdateArticleLikesCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedArticlesRepository.Object,
            mockedConfiguration.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }
}