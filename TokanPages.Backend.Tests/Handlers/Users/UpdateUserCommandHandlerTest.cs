﻿namespace TokanPages.Backend.Tests.Handlers.Users
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Commands.Users;
    using Shared.Services.DateTimeService;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class UpdateUserCommandHandlerTest : TestBase
    {
        private readonly DataUtilityService FDataUtilityService;

        public UpdateUserCommandHandlerTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public async Task GivenCorrectId_WhenUpdateUser_ShouldUpdateEntity()
        {
            // Arrange

            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = LUser.Id,
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
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
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
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
            var LTestEmail = FDataUtilityService.GetRandomEmail();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                EmailAddress = LTestEmail,
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataUtilityService.GetRandomString()
            };
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = LUser.Id,
                EmailAddress = LTestEmail,
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
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