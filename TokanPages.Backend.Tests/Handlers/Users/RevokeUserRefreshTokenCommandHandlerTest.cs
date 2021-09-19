namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Core.Exceptions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Cqrs.Services.UserServiceProvider;
    using MediatR;

    public class RevokeUserRefreshTokenCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidRefreshToken_WhenRevokeUserRefreshToken_ShouldSucceed()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LToken = DataUtilityService.GetRandomString(100);
            var LUser = new Users
            {
                Id = LUserId,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var LUserRefreshToken = new UserRefreshTokens
            {
                UserId = LUserId,
                Token = LToken,
                Expires = DateTimeService.Now,
                Created = DateTimeService.Now.AddMinutes(300),
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.UserRefreshTokens.AddAsync(LUserRefreshToken);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedIUserServiceProvider = new Mock<IUserServiceProvider>();

            var LRandomIpAddress = DataUtilityService.GetRandomIpAddress().ToString(); 
            LMockedIUserServiceProvider
                .Setup(AUserServiceProvider => AUserServiceProvider.GetRequestIpAddress())
                .Returns(LRandomIpAddress);

            LMockedIUserServiceProvider
                .Setup(AUserServiceProvider => AUserServiceProvider
                    .RevokeRefreshToken(
                        It.IsAny<UserRefreshTokens>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()));
            
            var LRevokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand { RefreshToken = LToken };
            var LRevokeUserRefreshTokenCommandHandler = new RevokeUserRefreshTokenCommandHandler(
                LDatabaseContext,
                LMockedIUserServiceProvider.Object
            );

            // Act
            var LResult = await LRevokeUserRefreshTokenCommandHandler.Handle(LRevokeUserRefreshTokenCommand, CancellationToken.None);

            // Assert
            LResult.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task GivenInvalidRefreshToken_WhenRevokeUserRefreshToken_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LToken = DataUtilityService.GetRandomString(100);
            var LUser = new Users
            {
                Id = LUserId,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedIUserServiceProvider = new Mock<IUserServiceProvider>();

            var LRandomIpAddress = DataUtilityService.GetRandomIpAddress().ToString(); 
            LMockedIUserServiceProvider
                .Setup(AUserServiceProvider => AUserServiceProvider.GetRequestIpAddress())
                .Returns(LRandomIpAddress);

            LMockedIUserServiceProvider
                .Setup(AUserServiceProvider => AUserServiceProvider
                    .RevokeRefreshToken(
                        It.IsAny<UserRefreshTokens>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()));
            
            var LRevokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand { RefreshToken = LToken };
            var LRevokeUserRefreshTokenCommandHandler = new RevokeUserRefreshTokenCommandHandler(
                LDatabaseContext,
                LMockedIUserServiceProvider.Object
            );

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LRevokeUserRefreshTokenCommandHandler.Handle(LRevokeUserRefreshTokenCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
        }
    }
}