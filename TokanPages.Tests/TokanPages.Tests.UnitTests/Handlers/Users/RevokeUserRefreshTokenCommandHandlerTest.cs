using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.CookieAccessorService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class RevokeUserRefreshTokenCommandHandlerTest : TestBase
{
    //TODO: re-write tests after reimplementation
    // [Fact]
    // public async Task GivenValidRefreshToken_WhenRevokeUserRefreshToken_ShouldSucceed()
    // {
    //     // Arrange
    //     var userId = Guid.NewGuid();
    //     var token = DataUtilityService.GetRandomString(100);
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
    //     var userRefreshToken = new UserRefreshToken
    //     {
    //         UserId = userId,
    //         Token = token,
    //         Expires = DateTimeService.Now,
    //         Created = DateTimeService.Now.AddMinutes(300),
    //         CreatedByIp = DataUtilityService.GetRandomIpAddress()
    //             .ToString(),
    //         RevokedByIp = string.Empty,
    //         ReplacedByToken = string.Empty,
    //         ReasonRevoked = string.Empty,
    //         Id = Guid.NewGuid()
    //     };
    //
    //     var databaseContext = GetTestDatabaseContext();
    //     await databaseContext.Users.AddAsync(user);
    //     await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
    //     await databaseContext.SaveChangesAsync();
    //
    //     var mockedUserService = new Mock<IUserService>();
    //     var mockedLogger = new Mock<ILoggerService>();
    //     var mockedCookieAccessor = new Mock<ICookieAccessor>();
    //     mockedCookieAccessor.Setup(accessor => accessor.Get(It.IsAny<string>()))
    //         .Returns(token);
    //
    //     var randomIpAddress = DataUtilityService.GetRandomIpAddress().ToString(); 
    //     mockedUserService
    //         .Setup(service => service.GetRequestIpAddress())
    //         .Returns(randomIpAddress);
    //
    //     mockedUserService
    //         .Setup(service => service.GetActiveUser(
    //             It.IsAny<Guid?>(), 
    //             It.IsAny<bool>(),
    //             It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(user);
    //
    //     mockedUserService
    //         .Setup(service => service
    //             .RevokeRefreshToken(
    //                 It.IsAny<RevokeRefreshTokenInput>(),
    //                 It.IsAny<CancellationToken>()));
    //         
    //     var command = new RevokeUserRefreshTokenCommand();
    //     var handler = new RevokeUserRefreshTokenCommandHandler(
    //         databaseContext,
    //         mockedLogger.Object,
    //         mockedUserService.Object, 
    //         mockedCookieAccessor.Object
    //     );
    //
    //     // Act
    //     var result = await handler.Handle(command, CancellationToken.None);
    //
    //     // Assert
    //     result.Should().Be(Unit.Value);
    // }
    //
    // [Fact]
    // public async Task GivenInvalidRefreshToken_WhenRevokeUserRefreshToken_ShouldThrowError()
    // {
    //     // Arrange
    //     var userId = Guid.NewGuid();
    //     var token = DataUtilityService.GetRandomString(100);
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
    //     var databaseContext = GetTestDatabaseContext();
    //     await databaseContext.Users.AddAsync(user);
    //     await databaseContext.SaveChangesAsync();
    //
    //     var mockedUserService = new Mock<IUserService>();
    //     var mockedLogger = new Mock<ILoggerService>();
    //     var mockedCookieAccessor = new Mock<ICookieAccessor>();
    //     mockedCookieAccessor.Setup(accessor => accessor.Get(It.IsAny<string>()))
    //         .Returns(token);
    //
    //     mockedUserService
    //         .Setup(service => service.GetActiveUser(
    //             It.IsAny<Guid?>(), 
    //             It.IsAny<bool>(),
    //             It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(user);
    //
    //     mockedUserService
    //         .Setup(service => service
    //             .RevokeRefreshToken(
    //                 It.IsAny<RevokeRefreshTokenInput>(),
    //                 It.IsAny<CancellationToken>()));
    //
    //     var command = new RevokeUserRefreshTokenCommand();
    //     var handler = new RevokeUserRefreshTokenCommandHandler(
    //         databaseContext,
    //         mockedLogger.Object,
    //         mockedUserService.Object, 
    //         mockedCookieAccessor.Object
    //     );
    //
    //     // Act
    //     // Assert
    //     var result = await Assert.ThrowsAsync<AuthorizationException>(() => handler.Handle(command, CancellationToken.None));
    //     result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
    // }
}