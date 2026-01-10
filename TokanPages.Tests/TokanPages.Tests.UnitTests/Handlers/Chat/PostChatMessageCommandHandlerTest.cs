using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Application.Chat.Models;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebSocketService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Chat;

public class PostChatMessageCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenChatKeyAndMessage_WhenPostChatMessage_ShouldSucceed()
    {
        // Arrange
        var userId0 = Guid.NewGuid();
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var chatKey = $"{userId1}:{userId2}".ToBase64Encode();
        var command = new PostChatMessageCommand
        {
            ChatKey = chatKey,
            Message = DataUtilityService.GetRandomString()
        };

        var users0 = new User
        {
            Id = userId0,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var users1 = new User
        {
            Id = userId1,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var users2 = new User
        {
            Id = userId2,
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var userInfo1 = new UserInfo
        {
            UserId = userId1,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = DataUtilityService.GetRandomString(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.NewGuid()
        };

        var userInfo2 = new UserInfo
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

        var userMessage = new UserMessage
        {
            Id = Guid.NewGuid(),
            ChatKey = chatKey,
            ChatData = serialized,
            IsArchived = false,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.NewGuid()
        };

        var users = new List<User>
        {
            users0,
            users1,
            users2
        };

        var userInfos = new List<UserInfo>
        {
            userInfo1,
            userInfo2
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserInfo.AddRangeAsync(userInfos);
        await databaseContext.UserMessages.AddAsync(userMessage);
        await databaseContext.SaveChangesAsync();

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service
                .GetActiveUser(
                    It.IsAny<Guid?>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(users0);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId0);

        var dateTime = DateTime.UtcNow;
        mockedDateTime
            .Setup(service => service.Now)
            .Returns(dateTime);

        var mockedNotification = new Mock<INotificationService>();
        var handler = new PostChatMessageCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedDateTime.Object,
            jsonSerializer,
            mockedUserService.Object,
            mockedNotification.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.DateTime.Should().Be(dateTime);
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