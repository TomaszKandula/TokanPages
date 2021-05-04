﻿using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;
using TokanPages.Backend.Core.Services.DateTimeService;

namespace Backend.UnitTests.Handlers.Users
{
    public class UpdateUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenUpdateUser_ShouldUpdateEntity()
        {
            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };

            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext, LMockedDateTime.Object);
            await LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LUserEntity = await LAssertDbContext.Users.FindAsync(LUpdateUserCommand.Id);

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
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.Parse("1edb4c7d-8cf0-4811-b721-af5caf74d7a8"),
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None));
        }

        [Fact]
        public async Task GivenExistingEmail_WhenUpdateUser_ShouldThrowError()
        {
            // Arrange
            var LTestEmail = DataProvider.GetRandomEmail();
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = LTestEmail,
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = LTestEmail,
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None));
        }
    }
}
