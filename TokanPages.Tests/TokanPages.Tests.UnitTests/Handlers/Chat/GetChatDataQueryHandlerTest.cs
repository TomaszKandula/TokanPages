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
    public async Task GivenChatKey_WhenGettingChatData_ShouldSucceed()
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

        var chatData = new List<GetChatItem>(2)
        {
            GetRandomChatItem(userId1, false, chatKey),
            GetRandomChatItem(userId2, false, chatKey)
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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users1);
        await databaseContext.Users.AddAsync(users2);
        await databaseContext.UserInfo.AddAsync(userInfo1);
        await databaseContext.UserInfo.AddAsync(userInfo2);
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
    }

    private GetChatItem GetRandomChatItem(Guid userId, bool isArchived, string chatKey)
    {
        return new GetChatItem
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ChatKey = chatKey,
            IsArchived = isArchived,
            AvatarName = DataUtilityService.GetRandomString(20),
            Initials = DataUtilityService.GetRandomString(2),
            DateTime = DataUtilityService.GetRandomDateTime(),
            Text = DataUtilityService.GetRandomString()
        };
    }
}