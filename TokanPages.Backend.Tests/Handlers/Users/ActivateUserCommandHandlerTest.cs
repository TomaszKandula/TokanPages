namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Shared;
    using Core.Utilities.LoggerService;
    using Core.Exceptions;
    using Domain.Entities;
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
            var activationId = Guid.NewGuid();
            var users = new Users
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString().ToLower(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTimeService.Now,
                AvatarName = Constants.Defaults.AvatarName,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ActivationId = activationId,
                ActivationIdEnds = DateTimeService.Now.AddMinutes(30)
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(users);
            await databaseContext.SaveChangesAsync();

            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedLogger = new Mock<ILoggerService>();

            mockedDateTimeService
                .SetupGet(service => service.Now)
                .Returns(DateTimeService.Now);
            
            var activateUserCommand = new ActivateUserCommand { ActivationId = activationId };
            var activateUserCommandHandler = new ActivateUserCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedDateTimeService.Object);

            // Act
            var result = await activateUserCommandHandler.Handle(activateUserCommand, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task GivenInvalidActivationId_WhenActivateUser_ShouldThrowError()
        {
            // Arrange
            var users = new Users
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString().ToLower(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTimeService.Now,
                AvatarName = Constants.Defaults.AvatarName,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ActivationId = Guid.NewGuid(),
                ActivationIdEnds = DateTimeService.Now.AddMinutes(30)
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(users);
            await databaseContext.SaveChangesAsync();

            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedLogger = new Mock<ILoggerService>();

            mockedDateTimeService
                .SetupGet(service => service.Now)
                .Returns(DateTimeService.Now);
            
            var activateUserCommand = new ActivateUserCommand { ActivationId = Guid.NewGuid() };
            var activateUserCommandHandler = new ActivateUserCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedDateTimeService.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => activateUserCommandHandler.Handle(activateUserCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_ACTIVATION_ID));
        }
        
        [Fact]
        public async Task GivenExpiredActivationId_WhenActivateUser_ShouldThrowError()
        {
            // Arrange
            var activationId = Guid.NewGuid();
            var users = new Users
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString().ToLower(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                Registered = DateTimeService.Now,
                AvatarName = Constants.Defaults.AvatarName,
                CryptedPassword = DataUtilityService.GetRandomString(),
                ActivationId = activationId,
                ActivationIdEnds = DateTimeService.Now.AddMinutes(-100)
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(users);
            await databaseContext.SaveChangesAsync();

            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedLogger = new Mock<ILoggerService>();

            mockedDateTimeService
                .SetupGet(service => service.Now)
                .Returns(DateTimeService.Now);
            
            var activateUserCommand = new ActivateUserCommand { ActivationId = activationId };
            var activateUserCommandHandler = new ActivateUserCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedDateTimeService.Object);

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => activateUserCommandHandler.Handle(activateUserCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID));
        }
    }
}