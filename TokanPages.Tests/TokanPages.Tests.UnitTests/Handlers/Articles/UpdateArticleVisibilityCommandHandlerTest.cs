using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
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
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedArticlesRepository
            .Setup(x => x.UpdateArticleVisibility(
            It.IsAny<Guid>(), 
            It.IsAny<Guid>(), 
            It.IsAny<DateTime>(),
            It.IsAny<bool>()))
            .ReturnsAsync(true);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedUserService
            .Setup(provider => provider.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new UpdateArticleVisibilityCommand
        {
            Id = articleId,
            IsPublished = true
        };

        var handler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedDateTime.Object,
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
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedArticlesRepository
            .Setup(x => x.UpdateArticleVisibility(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                It.IsAny<DateTime>(),
                It.IsAny<bool>()))
            .ReturnsAsync(true);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedUserService
            .Setup(provider => provider.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new UpdateArticleVisibilityCommand
        {
            Id = articleId,
            IsPublished = true
        };

        var handler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedDateTime.Object,
            mockedArticlesRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }

    [Fact]
    public async Task GivenWrongArticleId_WhenInvokeArticleVisibility_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedArticlesRepository = new Mock<IArticlesRepository>();

        mockedArticlesRepository
            .Setup(x => x.UpdateArticleVisibility(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                It.IsAny<DateTime>(),
                It.IsAny<bool>()))
            .ReturnsAsync(false);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId);

        mockedUserService
            .Setup(provider => provider.HasPermissionAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new UpdateArticleVisibilityCommand
        {
            Id = articleId,
            IsPublished = true
        };

        var handler = new UpdateArticleVisibilityCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object, 
            mockedDateTime.Object,
            mockedArticlesRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
    }
}