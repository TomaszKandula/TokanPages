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

public class GetUserNotesQueryHandlerTest : TestBase
{
    // TODO: redo
    // [Fact]
    // public async Task GivenUserId_WhenGetUserNotes_ShouldSucceed()
    // {
    //     // Arrange
    //     const string plainNote1 = "This is my test note... number one...";
    //     const string plainNote2 = "This is my test note... number two...";
    //     var compressedNote1 = plainNote1.CompressToBase64();
    //     var compressedNote2 = plainNote2.CompressToBase64();
    //
    //     var userId = Guid.NewGuid();
    //     var user = new User
    //     {
    //         Id = userId,
    //         EmailAddress = DataUtilityService.GetRandomEmail(),
    //         UserAlias = DataUtilityService.GetRandomString(),
    //         IsActivated = true,
    //         CryptedPassword = DataUtilityService.GetRandomString(),
    //         ResetId = null,
    //         CreatedBy = Guid.NewGuid(),
    //         CreatedAt = default,
    //         IsVerified = false,
    //         IsDeleted = false,
    //         HasBusinessLock = false
    //     };
    //
    //     var noteId1 = Guid.NewGuid();
    //     var noteId2 = Guid.NewGuid();
    //     var notes = new List<UserNote>
    //     {
    //         new()
    //         {
    //             Id = noteId1,
    //             UserId = userId,
    //             Note = compressedNote1,
    //             CreatedBy = userId,
    //             CreatedAt = DataUtilityService.GetRandomDateTime()
    //         },
    //         new()
    //         {
    //             Id = noteId2,
    //             UserId = userId,
    //             Note = compressedNote2,
    //             CreatedBy = userId,
    //             CreatedAt = DataUtilityService.GetRandomDateTime()
    //         }
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     await databaseContext.Users.AddAsync(user);
    //     await databaseContext.UserNotes.AddRangeAsync(notes);
    //     await databaseContext.SaveChangesAsync();
    //
    //     var mockedLogger = new Mock<ILoggerService>();
    //     var mockedUserService = new Mock<IUserService>();
    //     var mockUserRepository = new Mock<IUserRepository>();
    //
    //     mockedUserService
    //         .Setup(service => service.GetLoggedUserId())
    //         .Returns(userId);
    //
    //     var query = new GetUserNotesQuery();
    //     var handler = new GetUserNotesQueryHandler(databaseContext, mockedLogger.Object, mockedUserService.Object, mockUserRepository.Object);
    //
    //     // Act
    //     var result = await handler.Handle(query, CancellationToken.None);
    //
    //     // Assert
    //     result.Should().NotBeNull();
    //     result.Notes.Count.Should().Be(2);
    //
    //     var note1 = result.Notes
    //         .Where(queryResult => queryResult.Id == noteId1)
    //         .Select(queryResult => queryResult.Note)
    //         .SingleOrDefault();
    //
    //     var note2 = result.Notes
    //         .Where(queryResult => queryResult.Id == noteId2)
    //         .Select(queryResult => queryResult.Note)
    //         .SingleOrDefault();
    //
    //     note1.Should().NotBeNull();
    //     note2.Should().NotBeNull();
    //
    //     note1.Should().Be(plainNote1);
    //     note2.Should().Be(plainNote2);
    // }
}