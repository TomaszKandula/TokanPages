using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
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
        var user = new User
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = default,
            IsVerified = false,
            IsDeleted = false,
            HasBusinessLock = false
        };

        var note = new UserNote
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
        var mockUserRepository = new Mock<IUserRepository>();

        mockedUserService
            .Setup(service => service.GetActiveUser(It.IsAny<Guid?>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(user.Id);

        var query = new GetUserNoteQuery
        {
            UserNoteId = note.Id
        };

        var handler = new GetUserNoteQueryHandler(databaseContext, mockedLogger.Object, mockedUserService.Object, mockUserRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Note.Should().Be(plainNote);
    }

    [Fact]
    public async Task GivenWrongNoteId_WhenGetUserNote_ShouldReturnEmptyObject()
    {
        // Arrange
        const string plainNote = "This is my test note...";
        var compressedNote = plainNote.CompressToBase64();

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = default,
            IsVerified = false,
            IsDeleted = false,
            HasBusinessLock = false
        };

        var note = new UserNote
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
        var mockUserRepository = new Mock<IUserRepository>();

        mockedUserService
            .Setup(service => service.GetActiveUser(It.IsAny<Guid?>()))
            .ReturnsAsync(user);

        var query = new GetUserNoteQuery
        {
            UserNoteId = Guid.NewGuid()
        };

        var handler = new GetUserNoteQueryHandler(databaseContext, mockedLogger.Object, mockedUserService.Object, mockUserRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Note.Should().BeEmpty();
    }
}