namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading.Tasks;
    using Shared;
    using System.Threading;
    using Core.Logger;
    using Core.Exceptions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Core.Utilities.DateTimeService;
    using MediatR;

    public class ActivateUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidActivationId_WhenActivateUser_ShouldFinishSuccessful()
        {
            // Arrange
            var LActivationId = Guid.NewGuid();
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString().ToLower(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTimeService.Now,
                AvatarName = Constants.Defaults.AVATAR_NAME,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ActivationId = LActivationId,
                ActivationIdEnds = DateTimeService.Now.AddMinutes(30)
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedLogger = new Mock<ILogger>();

            LMockedDateTimeService
                .SetupGet(AService => AService.Now)
                .Returns(DateTimeService.Now);
            
            var LActivateUserCommand = new ActivateUserCommand { ActivationId = LActivationId };
            var LActivateUserCommandHandler = new ActivateUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTimeService.Object, 
                LMockedLogger.Object);

            // Act
            var LResult = await LActivateUserCommandHandler.Handle(LActivateUserCommand, CancellationToken.None);

            // Assert
            LResult.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task GivenInvalidActivationId_WhenActivateUser_ShouldThrowError()
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString().ToLower(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTimeService.Now,
                AvatarName = Constants.Defaults.AVATAR_NAME,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ActivationId = Guid.NewGuid(),
                ActivationIdEnds = DateTimeService.Now.AddMinutes(30)
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedLogger = new Mock<ILogger>();

            LMockedDateTimeService
                .SetupGet(AService => AService.Now)
                .Returns(DateTimeService.Now);
            
            var LActivateUserCommand = new ActivateUserCommand { ActivationId = Guid.NewGuid() };
            var LActivateUserCommandHandler = new ActivateUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTimeService.Object, 
                LMockedLogger.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LActivateUserCommandHandler.Handle(LActivateUserCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_ACTIVATION_ID));
        }
        
        [Fact]
        public async Task GivenExpiredActivationId_WhenActivateUser_ShouldThrowError()
        {
            // Arrange
            var LActivationId = Guid.NewGuid();
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString().ToLower(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTimeService.Now,
                AvatarName = Constants.Defaults.AVATAR_NAME,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ActivationId = LActivationId,
                ActivationIdEnds = DateTimeService.Now.AddMinutes(-100)
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedLogger = new Mock<ILogger>();

            LMockedDateTimeService
                .SetupGet(AService => AService.Now)
                .Returns(DateTimeService.Now);
            
            var LActivateUserCommand = new ActivateUserCommand { ActivationId = LActivationId };
            var LActivateUserCommandHandler = new ActivateUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTimeService.Object, 
                LMockedLogger.Object);

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LActivateUserCommandHandler.Handle(LActivateUserCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID));
        }
    }
}