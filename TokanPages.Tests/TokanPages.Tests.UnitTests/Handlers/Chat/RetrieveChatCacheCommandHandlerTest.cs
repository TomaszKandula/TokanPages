using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Users;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Chat;

public class RetrieveChatCacheCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenChatKeys_WhenRetrieveChatCache_ShouldSucceed()
    {
        // Arrange
        var key1 = DataUtilityService.GetRandomString();
        var key2 = DataUtilityService.GetRandomString();
        var chatKeys = new[] { key1, key2 };
        var command = new RetrieveChatCacheCommand
        {
            ChatKey = chatKeys
        };

        var userMessagesCache1 = new UserMessageCache
        {
            ChatKey = key1,
            Notification = DataUtilityService.GetRandomString()
        };

        var userMessagesCache2 = new UserMessageCache
        {
            ChatKey = key2,
            Notification = DataUtilityService.GetRandomString()
        };

        var userMessageCache = new List<UserMessageCache>
        {
            userMessagesCache1,
            userMessagesCache2
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.UserMessagesCache.AddRangeAsync(userMessageCache);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new RetrieveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Notifications.Length.Should().Be(2);
    }

    [Fact]
    public async Task GivenNoChatKeys_WhenRetrieveChatCache_ShouldReturnEmptyArray()
    {
        // Arrange
        var key1 = DataUtilityService.GetRandomString();
        var key2 = DataUtilityService.GetRandomString();
        var chatKeys = new[] { key1, key2 };
        var command = new RetrieveChatCacheCommand
        {
            ChatKey = chatKeys
        };

        var userMessagesCache1 = new UserMessageCache
        {
            ChatKey = DataUtilityService.GetRandomString(),
            Notification = DataUtilityService.GetRandomString()
        };

        var userMessagesCache2 = new UserMessageCache
        {
            ChatKey = DataUtilityService.GetRandomString(),
            Notification = DataUtilityService.GetRandomString()
        };

        var userMessageCache = new List<UserMessageCache>
        {
            userMessagesCache1,
            userMessagesCache2
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.UserMessagesCache.AddRangeAsync(userMessageCache);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new RetrieveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Notifications.Length.Should().Be(0);
    }
}