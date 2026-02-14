using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class GetUserQueryHandlerTest : TestBase
{
    // TODO: redo
    // [Fact]
    // public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntity() 
    // {
    //     // Arrange
    //     var users = new User
    //     {
    //         Id = Guid.NewGuid(),
    //         EmailAddress = DataUtilityService.GetRandomEmail(),
    //         UserAlias = DataUtilityService.GetRandomString(),
    //         IsActivated = true,
    //         CryptedPassword = DataUtilityService.GetRandomString(),
    //         CreatedAt = DataUtilityService.GetRandomDateTime(),
    //         CreatedBy = Guid.Empty,
    //         ResetId = null,
    //         IsVerified = false,
    //         IsDeleted = false,
    //         HasBusinessLock = false
    //     };
    //
    //     var userInfo = new UserInfo
    //     {
    //         UserId = users.Id,
    //         FirstName = DataUtilityService.GetRandomString(),
    //         LastName = DataUtilityService.GetRandomString(),
    //         UserAboutText = DataUtilityService.GetRandomString(),
    //         UserImageName = string.Empty,
    //         UserVideoName = string.Empty,
    //         CreatedBy = Guid.Empty,
    //         CreatedAt = DataUtilityService.GetRandomDateTime(),
    //         ModifiedBy = null,
    //         ModifiedAt = null,
    //         Id = Guid.NewGuid()
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     await databaseContext.Users.AddAsync(users);
    //     await databaseContext.UserInformation.AddAsync(userInfo);
    //     await databaseContext.SaveChangesAsync();
    //
    //     var mockedLogger = new Mock<ILoggerService>();
    //     var mockUserRepository = new Mock<IUserRepository>();
    //     var query = new GetUserQuery { Id = users.Id };
    //     var handler = new GetUserQueryHandler(databaseContext, mockedLogger.Object, mockUserRepository.Object);
    //
    //     // Act
    //     var result = await handler.Handle(query, CancellationToken.None);
    //
    //     // Assert
    //     result.Should().NotBeNull();
    //     result.Email.Should().Be(users.EmailAddress);
    //     result.AliasName.Should().Be(users.UserAlias);
    //     result.IsActivated.Should().BeTrue();
    //     result.Registered.Should().Be(users.CreatedAt);
    //     result.LastUpdated.Should().BeNull();
    //     result.LastLogged.Should().BeNull();
    // }

    [Fact]
    public async Task GivenIncorrectId_WhenGetUser_ShouldThrowError()
    {
        // Arrange
        var mockedLogger = new Mock<ILoggerService>();
        var mockUserRepository = new Mock<IUserRepository>();

        var query = new GetUserQuery { Id = Guid.NewGuid() };
        var handler = new GetUserQueryHandler(mockedLogger.Object, mockUserRepository.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
    }
}