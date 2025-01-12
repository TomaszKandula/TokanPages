using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class GetUserNoteQueryHandlerTest : TestBase
{
    [Fact]
    public async Task GivenNoteId_WhenGetUserNote_ShouldSucceed()
    {
        // Arrange
        const string plainNote = "This is my test note...";
        var compressedNote = plainNote.CompressToBase64();

        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var note = new Backend.Domain.Entities.User.UserNote
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Note = compressedNote,
            CreatedBy = userId,
            CreatedAt = DataUtilityService.GetRandomDateTime()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserNotes.AddAsync(note);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var query = new GetUserNoteQuery
        {
            UserNoteId = note.Id
        };

        var handler = new GetUserNoteQueryHandler(databaseContext, mockedLogger.Object, mockedUserService.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Note.Should().Be(plainNote);
    }
}