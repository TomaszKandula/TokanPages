using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Chat.Models;
using TokanPages.Backend.Application.Chat.Queries;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Chat;

public class GetChatDataQueryHandlerTest : TestBase
{
    [Fact]
    public async Task GivenChatKeyAndUserInfo_WhenGettingChatData_ShouldSucceed()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var chatKey = $"{userId1}:{userId2}".ToBase64Encode();
        var query = new GetChatDataQuery { ChatKey = chatKey };

        var users1 = new Backend.Domain.Entities.User.Users
        {
            Id = userId1,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var users2 = new Backend.Domain.Entities.User.Users
        {
            Id = userId2,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var userInfo1 = new Backend.Domain.Entities.User.UserInfo
        {
            UserId = userId1,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = DataUtilityService.GetRandomString(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.NewGuid()
        };

        var userInfo2 = new Backend.Domain.Entities.User.UserInfo
        {
            UserId = userId2,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = DataUtilityService.GetRandomString(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.NewGuid()
        };

        var user1ChatData = GetRandomChatItem(userId1, false, chatKey);
        var user2ChatData = GetRandomChatItem(userId2, false, chatKey);
        var chatData = new List<GetChatItem>(2)
        {
            user1ChatData,
            user2ChatData
        };

        var jsonSerializer = new JsonSerializer();
        var serialized = jsonSerializer.Serialize(chatData);

        var userMessage = new Backend.Domain.Entities.User.UserMessage
        {
            Id = Guid.NewGuid(),
            ChatKey = chatKey,
            ChatData = serialized,
            IsArchived = false,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.NewGuid()
        };

        var users = new List<Backend.Domain.Entities.User.Users>
        {
            users1,
            users2
        };

        var userInfos = new List<Backend.Domain.Entities.User.UserInfo>
        {
            userInfo1,
            userInfo2
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserInfo.AddRangeAsync(userInfos);
        await databaseContext.UserMessages.AddAsync(userMessage);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new GetChatDataQueryHandler(
            databaseContext, 
            mockedLogger.Object, 
            jsonSerializer);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ChatData.Should().NotBeNull();
        result.ChatData.Should().NotBeEmpty();

        var testChatData = jsonSerializer.Deserialize<List<GetChatItem>>(result.ChatData);
        testChatData.Count.Should().Be(2);
        testChatData[0].UserId.Should().Be(userId1);
        testChatData[0].ChatKey.Should().Be(chatKey);
        testChatData[0].Initials.Should().NotBe("A");
        testChatData[0].AvatarName.Should().Be(userInfo1.UserImageName);
        testChatData[0].Text.Should().Be(user1ChatData.Text);
        testChatData[1].UserId.Should().Be(userId2);
        testChatData[1].ChatKey.Should().Be(chatKey);
        testChatData[1].Initials.Should().NotBe("A");
        testChatData[1].AvatarName.Should().Be(userInfo2.UserImageName);
        testChatData[1].Text.Should().Be(user2ChatData.Text);
    }

    [Fact]
    public async Task GivenChatKeyAndNoUserInfo_WhenGettingChatData_ShouldSucceed()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var chatKey = $"{userId1}:{userId2}".ToBase64Encode();
        var query = new GetChatDataQuery { ChatKey = chatKey };

        var users1 = new Backend.Domain.Entities.User.Users
        {
            Id = userId1,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var users2 = new Backend.Domain.Entities.User.Users
        {
            Id = userId2,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var user1ChatData = GetRandomChatItem(userId1, false, chatKey);
        var user2ChatData = GetRandomChatItem(userId2, false, chatKey);
        var chatData = new List<GetChatItem>(2)
        {
            user1ChatData,
            user2ChatData
        };

        var jsonSerializer = new JsonSerializer();
        var serialized = jsonSerializer.Serialize(chatData);

        var userMessage = new Backend.Domain.Entities.User.UserMessage
        {
            Id = Guid.NewGuid(),
            ChatKey = chatKey,
            ChatData = serialized,
            IsArchived = false,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.NewGuid()
        };

        var users = new List<Backend.Domain.Entities.User.Users>
        {
            users1,
            users2
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserMessages.AddAsync(userMessage);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new GetChatDataQueryHandler(
            databaseContext, 
            mockedLogger.Object, 
            jsonSerializer);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ChatData.Should().NotBeNull();
        result.ChatData.Should().NotBeEmpty();

        var testChatData = jsonSerializer.Deserialize<List<GetChatItem>>(result.ChatData);
        testChatData.Count.Should().Be(2);
        testChatData[0].UserId.Should().Be(userId1);
        testChatData[0].ChatKey.Should().Be(chatKey);
        testChatData[0].Initials.Should().Be("A");
        testChatData[0].AvatarName.Should().BeEmpty();
        testChatData[0].Text.Should().Be(user1ChatData.Text);
        testChatData[1].UserId.Should().Be(userId2);
        testChatData[1].ChatKey.Should().Be(chatKey);
        testChatData[1].Initials.Should().Be("A");
        testChatData[1].AvatarName.Should().BeEmpty();
        testChatData[1].Text.Should().Be(user2ChatData.Text);
    }

    [Fact]
    public async Task GivenUnexistingChatKey_WhenGettingChatData_ShouldReturnEmptyObject()
    {
        // Arrange
        var query = new GetChatDataQuery { ChatKey = "SOME_OTHER_CHAT_KEY" };
        var databaseContext = GetTestDatabaseContext();
        var mockedSerializer = new Mock<IJsonSerializer>();
        var mockedLogger = new Mock<ILoggerService>();
        var handler = new GetChatDataQueryHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedSerializer.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ChatData.Should().NotBeNull();
        result.ChatData.Should().BeEmpty();
    }
    
    private GetChatItem GetRandomChatItem(Guid userId, bool isArchived, string chatKey)
    {
        return new GetChatItem
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ChatKey = chatKey,
            IsArchived = isArchived,
            DateTime = DataUtilityService.GetRandomDateTime(),
            Text = DataUtilityService.GetRandomString()
        };
    }
}