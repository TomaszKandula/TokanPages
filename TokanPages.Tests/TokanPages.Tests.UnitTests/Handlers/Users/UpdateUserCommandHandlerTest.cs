using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class UpdateUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenUpdateUser_ShouldUpdateEntity()
    {
        // Arrange
        var user = new User
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = default,
            IsVerified = false,
            IsDeleted = false,
            HasBusinessLock = false,
            Id = Guid.NewGuid()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var command = new UpdateUserCommand
        {
            Id = user.Id,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new UpdateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedUserService.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var userEntity = await databaseContext.Users.FindAsync(command.Id);

        userEntity.Should().NotBeNull();
        userEntity?.EmailAddress.Should().Be(command.EmailAddress);
        userEntity?.UserAlias.Should().Be(command.UserAlias);
        userEntity?.IsActivated.Should().BeTrue();
    }

    [Fact]
    public async Task GivenExistingEmail_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var user = new List<User>
        {
            new()
            {
                EmailAddress = testEmail,
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ResetId = null,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = default,
                IsVerified = false,
                IsDeleted = false,
                HasBusinessLock = false,
                Id = Guid.NewGuid()
            },
            new()
            {
                EmailAddress = testEmail,
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ResetId = null,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = default,
                IsVerified = false,
                IsDeleted = false,
                HasBusinessLock = false,
                Id = Guid.NewGuid()
            },
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(user);
        await databaseContext.SaveChangesAsync();

        var command = new UpdateUserCommand
        {
            Id = user[0].Id,
            EmailAddress = testEmail,
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user[1]);

        var handler = new UpdateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
    }
}