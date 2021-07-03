using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;
using TokanPages.Backend.Shared.Services.DateTimeService;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Handlers.Users
{
    public class UpdateUserCommandHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public UpdateUserCommandHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenCorrectId_WhenUpdateUser_ShouldUpdateEntity()
        {
            // Arrange

            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = LUser.Id,
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
            };

            // Act
            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext, LMockedDateTime.Object);
            await LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None);

            // Assert
            var LUserEntity = await LDatabaseContext.Users.FindAsync(LUpdateUserCommand.Id);

            LUserEntity.Should().NotBeNull();
            LUserEntity.EmailAddress.Should().Be(LUpdateUserCommand.EmailAddress);
            LUserEntity.UserAlias.Should().Be(LUpdateUserCommand.UserAlias);
            LUserEntity.FirstName.Should().Be(LUpdateUserCommand.FirstName);
            LUserEntity.LastName.Should().Be(LUpdateUserCommand.LastName);
            LUserEntity.IsActivated.Should().BeTrue();
            LUserEntity.LastUpdated.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.Parse("1edb4c7d-8cf0-4811-b721-af5caf74d7a8"),
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
            };

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None));
        }

        [Fact]
        public async Task GivenExistingEmail_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LTestEmail = FDataProviderService.GetRandomEmail();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = LTestEmail,
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = LUser.Id,
                EmailAddress = LTestEmail,
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
            };

            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None));
        }
    }
}
