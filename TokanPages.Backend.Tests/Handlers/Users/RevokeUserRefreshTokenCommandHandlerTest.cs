namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Logger;
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
            var userId = Guid.NewGuid();
            var token = DataUtilityService.GetRandomString(100);
            var user = new Users
            {
                Id = userId,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var userRefreshToken = new UserRefreshTokens
            {
                UserId = userId,
                Token = token,
                Expires = DateTimeService.Now,
                Created = DateTimeService.Now.AddMinutes(300),
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString()
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
            await databaseContext.SaveChangesAsync();

            var mockedIUserServiceProvider = new Mock<IUserServiceProvider>();
            var mockedLogger = new Mock<ILogger>();

            var randomIpAddress = DataUtilityService.GetRandomIpAddress().ToString(); 
            mockedIUserServiceProvider
                .Setup(provider => provider.GetRequestIpAddress())
                .Returns(randomIpAddress);

            mockedIUserServiceProvider
                .Setup(provider => provider
                    .RevokeRefreshToken(
                        It.IsAny<UserRefreshTokens>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()));
            
            var revokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand { RefreshToken = token };
            var revokeUserRefreshTokenCommandHandler = new RevokeUserRefreshTokenCommandHandler(
                databaseContext,
                mockedLogger.Object,
                mockedIUserServiceProvider.Object
            );

            // Act
            var result = await revokeUserRefreshTokenCommandHandler.Handle(revokeUserRefreshTokenCommand, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task GivenInvalidRefreshToken_WhenRevokeUserRefreshToken_ShouldThrowError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var token = DataUtilityService.GetRandomString(100);
            var user = new Users
            {
                Id = userId,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var mockedIUserServiceProvider = new Mock<IUserServiceProvider>();
            var mockedLogger = new Mock<ILogger>();

            var randomIpAddress = DataUtilityService.GetRandomIpAddress().ToString(); 
            mockedIUserServiceProvider
                .Setup(provider => provider.GetRequestIpAddress())
                .Returns(randomIpAddress);

            mockedIUserServiceProvider
                .Setup(provider => provider
                    .RevokeRefreshToken(
                        It.IsAny<UserRefreshTokens>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()));
            
            var revokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand { RefreshToken = token };
            var revokeUserRefreshTokenCommandHandler = new RevokeUserRefreshTokenCommandHandler(
                databaseContext,
                mockedLogger.Object,
                mockedIUserServiceProvider.Object
            );

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => revokeUserRefreshTokenCommandHandler.Handle(revokeUserRefreshTokenCommand, CancellationToken.None));

            result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
        }
    }
}