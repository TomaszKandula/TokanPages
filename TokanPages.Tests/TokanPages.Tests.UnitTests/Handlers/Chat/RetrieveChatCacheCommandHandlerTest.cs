using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Repositories.Chat;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Chat;

public class RetrieveChatCacheCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenChatKeys_WhenRetrieveChatCache_ShouldSucceed()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var key1 = DataUtilityService.GetRandomString();
        var key2 = DataUtilityService.GetRandomString();
        var chatKeys = new[] { key1, key2 };
        var command = new RetrieveChatCacheCommand
        {
            ChatKey = chatKeys
        };

        var userMessageCache = new []
        {
            DataUtilityService.GetRandomString(),
            DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedChatRepository = new Mock<IChatRepository>();

        mockedChatRepository
            .Setup(repository => repository.RetrieveChatCache(It.IsAny<string[]>()))
            .ReturnsAsync(userMessageCache);

        var handler = new RetrieveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedChatRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Notifications.Length.Should().Be(2);
    }

    [Fact]
    public async Task GivenNoChatKeys_WhenRetrieveChatCache_ShouldReturnEmptyArray()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var key1 = DataUtilityService.GetRandomString();
        var key2 = DataUtilityService.GetRandomString();
        var chatKeys = new[] { key1, key2 };
        var command = new RetrieveChatCacheCommand
        {
            ChatKey = chatKeys
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedChatRepository = new Mock<IChatRepository>();

        mockedChatRepository
            .Setup(repository => repository.RetrieveChatCache(It.IsAny<string[]>()))
            .ReturnsAsync(Array.Empty<string>());

        var handler = new RetrieveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedChatRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Notifications.Length.Should().Be(0);
    }
}