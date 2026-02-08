using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Chat;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Chat;

public class RemoveChatCacheCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenChatKeys_WhenRemoveChatCache_ShouldSucceed()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var key = DataUtilityService.GetRandomString();
        var command = new RemoveChatCacheCommand
        {
            ChatKey = key
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedChatRepository = new Mock<IChatRepository>();

        mockedChatRepository
            .Setup(repository => repository.DeleteChatUserCacheByKey(It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var handler = new RemoveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedChatRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenChatId_WhenRemoveChatCache_ShouldSucceed()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var command = new RemoveChatCacheCommand
        {
            ChatId = Guid.NewGuid()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedChatRepository = new Mock<IChatRepository>();

        mockedChatRepository
            .Setup(repository => repository.DeleteChatUserCacheById(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        var handler = new RemoveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedChatRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenNoIdAndNoKey_WhenRemoveChatCache_ShouldSucceed()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var command = new RemoveChatCacheCommand
        {
            ChatKey = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedChatRepository = new Mock<IChatRepository>();

        var handler = new RemoveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedChatRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }
}