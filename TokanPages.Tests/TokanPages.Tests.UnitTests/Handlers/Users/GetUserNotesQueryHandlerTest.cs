using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class GetUserNotesQueryHandlerTest : TestBase
{
    [Fact]
    public async Task GivenUserId_WhenGetUserNotes_ShouldSucceed()
    {
        // Arrange
        const string plainNote1 = "This is my test note... number one...";
        const string plainNote2 = "This is my test note... number two...";
        var compressedNote1 = plainNote1.CompressToBase64();
        var compressedNote2 = plainNote2.CompressToBase64();

        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var notes = new List<UserNote>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Note = compressedNote1,
                CreatedBy = userId,
                CreatedAt = DataUtilityService.GetRandomDateTime()
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Note = compressedNote2,
                CreatedBy = userId,
                CreatedAt = DataUtilityService.GetRandomDateTime()
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserNotes.AddRangeAsync(notes);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var query = new GetUserNotesQuery();
        var handler = new GetUserNotesQueryHandler(databaseContext, mockedLogger.Object, mockedUserService.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserNotes.Count.Should().Be(2);
        result.UserNotes[0].Note.Should().Be(plainNote1);
        result.UserNotes[1].Note.Should().Be(plainNote2);
    }
}