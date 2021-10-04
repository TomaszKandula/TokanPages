namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Logger;
    using Core.Exceptions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Cqrs.Services.CipheringService;
    using Core.Utilities.DateTimeService;
    using Cqrs.Services.UserServiceProvider;

    public class UpdateUserPasswordCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidUserDataAndNewPasswordAsLoggedUser_WhenUpdateUserPassword_ShouldFinishSuccessful()
        {
            // Arrange
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = string.Empty,
                ResetId = null,
                ResetIdEnds = null,
                ActivationId = null,
                ActivationIdEnds = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = LUser.Id,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserProvider = new Mock<IUserServiceProvider>();
            var LMockedCipheringService = new Mock<ICipheringService>();

            LMockedUserProvider
                .Setup(AService => AService.HasRoleAssigned(It.IsAny<string>()))
                .ReturnsAsync(true);
            
            var LMockedPassword = DataUtilityService.GetRandomString();
            LMockedCipheringService
                .Setup(AService => AService.GetHashedPassword(
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
                .Returns(LMockedPassword);

            LMockedCipheringService
                .Setup(AService => AService.GenerateSalt(It.IsAny<int>()))
                .Returns(string.Empty);
            
            var LUpdateUserCommandHandler = new UpdateUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object,
                LMockedCipheringService.Object,
                LMockedDateTimeService.Object,
                LMockedLogger.Object
            );

            // Act
            await LUpdateUserCommandHandler.Handle(LUpdateUserPasswordCommand, CancellationToken.None);

            // Assert
            var LUserEntity = await LDatabaseContext.Users.FindAsync(LUser.Id);
            
            LUserEntity.Should().NotBeNull();
            LUserEntity.CryptedPassword.Should().NotBeEmpty();
            LUserEntity.ResetId.Should().BeNull();
        }

        [Fact]
        public async Task GivenInvalidResetId_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = string.Empty,
                ResetId = Guid.NewGuid(),
                ResetIdEnds = DateTime.Now.AddMinutes(30),
                ActivationId = null,
                ActivationIdEnds = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = LUser.Id,
                ResetId = Guid.NewGuid(),
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserProvider = new Mock<IUserServiceProvider>();
            var LMockedCipheringService = new Mock<ICipheringService>();

            var LMockedPassword = DataUtilityService.GetRandomString();
            LMockedCipheringService
                .Setup(AService => AService.GetHashedPassword(
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
                .Returns(LMockedPassword);

            LMockedCipheringService
                .Setup(AService => AService.GenerateSalt(It.IsAny<int>()))
                .Returns(string.Empty);
            
            var LUpdateUserCommandHandler = new UpdateUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object,
                LMockedCipheringService.Object,
                LMockedDateTimeService.Object,
                LMockedLogger.Object
            );

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateUserCommandHandler.Handle(LUpdateUserPasswordCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_RESET_ID));
        }

        [Fact]
        public async Task GivenInvalidUserId_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = string.Empty,
                ResetId = null,
                ResetIdEnds = null,
                ActivationId = null,
                ActivationIdEnds = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = Guid.NewGuid(),
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserProvider = new Mock<IUserServiceProvider>();
            var LMockedCipheringService = new Mock<ICipheringService>();

            var LMockedPassword = DataUtilityService.GetRandomString();
            LMockedCipheringService
                .Setup(AService => AService.GetHashedPassword(
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
                .Returns(LMockedPassword);

            LMockedCipheringService
                .Setup(AService => AService.GenerateSalt(It.IsAny<int>()))
                .Returns(string.Empty);
            
            var LUpdateUserCommandHandler = new UpdateUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object,
                LMockedCipheringService.Object,
                LMockedDateTimeService.Object,
                LMockedLogger.Object
            );

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateUserCommandHandler.Handle(LUpdateUserPasswordCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
        }

        [Fact]
        public async Task GivenNoResetIdAsNotLoggedUser_WhenUpdateUserPassword_ShouldThrowError()
        {
            // Arrange
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ResetId = null,
                ResetIdEnds = null,
                ActivationId = null,
                ActivationIdEnds = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserPasswordCommand = new UpdateUserPasswordCommand
            {
                Id = LUser.Id,
                ResetId = null,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LMockedLogger = new Mock<ILogger>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserProvider = new Mock<IUserServiceProvider>();
            var LMockedCipheringService = new Mock<ICipheringService>();

            LMockedUserProvider
                .Setup(AProvider => AProvider.HasRoleAssigned(It.IsAny<string>()))
                .ReturnsAsync(false);
            
            var LMockedPassword = DataUtilityService.GetRandomString();
            LMockedCipheringService
                .Setup(AService => AService.GetHashedPassword(
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
                .Returns(LMockedPassword);

            LMockedCipheringService
                .Setup(AService => AService.GenerateSalt(It.IsAny<int>()))
                .Returns(string.Empty);
            
            var LUpdateUserCommandHandler = new UpdateUserPasswordCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object,
                LMockedCipheringService.Object,
                LMockedDateTimeService.Object,
                LMockedLogger.Object
            );

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateUserCommandHandler.Handle(LUpdateUserPasswordCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
    }
}