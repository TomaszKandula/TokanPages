using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Chat.Commands;
using TokanPages.Backend.Application.Chat.Models;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Persistence.DataAccess.Repositories.Chat;
using TokanPages.Persistence.DataAccess.Repositories.Chat.Models;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebSocketService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Chat;

public class PostChatMessageCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenChatKeyAndMessage_WhenPostChatMessage_ShouldSucceed()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var chatKey = $"{userId1}:{userId2}".ToBase64Encode();
        var command = new PostChatMessageCommand
        {
            ChatKey = chatKey,
            Message = DataUtilityService.GetRandomString()
        };

        var userInfo1 = new UserInfo
        {
            UserId = userId1,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = DataUtilityService.GetRandomString(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.NewGuid(),
            UserVideoName = string.Empty,
            Id = Guid.NewGuid()
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

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedNotification = new Mock<INotificationService>();
        var mockedChatRepository = new Mock<IChatRepository>();

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(userId1);

        var dateTime = DataUtilityService.GetRandomDateTime();
        mockedDateTime
            .Setup(service => service.Now)
            .Returns(dateTime);

        mockedChatRepository
            .Setup(repository => repository.GetChatUserMessageData(
                It.IsAny<string>(),
                It.IsAny<bool>()))
            .ReturnsAsync(userMessage);

        mockedChatRepository
            .Setup(repository => repository.GetChatUserData(userId1))
            .ReturnsAsync(new ChatUserDataDto
            {
                FirstName = userInfo1.FirstName,
                LastName = userInfo1.LastName,
                UserImageName = userInfo1.UserImageName,
            });

        mockedChatRepository
            .Setup(repository => repository.UpdateChatUserMessageData(
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<bool>(), 
            It.IsAny<DateTime>(), 
            It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        mockedChatRepository
            .Setup(repository => repository.CreateChatUserData(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<bool>(), 
                It.IsAny<DateTime>(), 
                It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        var handler = new PostChatMessageCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedDateTime.Object,
            jsonSerializer,
            mockedUserService.Object,
            mockedNotification.Object,
            mockedChatRepository.Object);

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