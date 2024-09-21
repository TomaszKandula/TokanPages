using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Chat;

public class RemoveChatCacheCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenChatKeys_WhenRetrieveChatCache_ShouldSucceed()
    {
        // Arrange
        var key = DataUtilityService.GetRandomString();
        var command = new RemoveChatCacheCommand
        {
            ChatKey = key
        };

        var userMessagesCache = new UserMessageCache
        {
            ChatKey = key,
            Notification = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.UserMessagesCache.AddAsync(userMessagesCache);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new RemoveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        var cache = databaseContext.UserMessagesCache.ToList();
        cache.Count.Should().Be(0);
    }

    [Fact]
    public async Task GivenChatId_WhenRetrieveChatCache_ShouldSucceed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var key = DataUtilityService.GetRandomString();
        var command = new RemoveChatCacheCommand
        {
            ChatId = id
        };

        var userMessagesCache = new UserMessageCache
        {
            Id = id,
            ChatKey = key,
            Notification = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.UserMessagesCache.AddAsync(userMessagesCache);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new RemoveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        var cache = databaseContext.UserMessagesCache.ToList();
        cache.Count.Should().Be(0);
    }

    [Fact]
    public async Task GivenNoIdAndNoKey_WhenRetrieveChatCache_ShouldSucceed()
    {
        // Arrange
        var key = DataUtilityService.GetRandomString();
        var command = new RemoveChatCacheCommand
        {
            ChatKey = DataUtilityService.GetRandomString()
        };

        var userMessagesCache = new UserMessageCache
        {
            ChatKey = key,
            Notification = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.UserMessagesCache.AddAsync(userMessagesCache);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new RemoveChatCacheCommandHandler(
            databaseContext, 
            mockedLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        var cache = databaseContext.UserMessagesCache.ToList();
        cache.Count.Should().Be(1);
    }
}