using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Articles;

public class UpdateArticleVisibilityCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenExistingArticle_WhenInvokeArticleVisibility_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedArticlesRepository
            .Setup(x => x.UpdateArticleVisibility(
            It.IsAny<Guid>(), 
            It.IsAny<Guid>(), 
            It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedUserService
            .Setup(provider => provider.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>()))
            .ReturnsAsync(true);

        var command = new UpdateArticleVisibilityCommand
        {
            Id = articleId,
            IsPublished = true
        };

        var handler = new UpdateArticleVisibilityCommandHandler(
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedArticlesRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);        
    }

    [Fact]
    public async Task GivenNoPermission_WhenInvokeArticleVisibility_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedArticlesRepository
            .Setup(x => x.UpdateArticleVisibility(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                It.IsAny<bool>()))
            .Returns(Task.CompletedTask);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedUserService
            .Setup(provider => provider.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>()))
            .ReturnsAsync(false);

        var command = new UpdateArticleVisibilityCommand
        {
            Id = articleId,
            IsPublished = true
        };

        var handler = new UpdateArticleVisibilityCommandHandler(
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedArticlesRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }
}