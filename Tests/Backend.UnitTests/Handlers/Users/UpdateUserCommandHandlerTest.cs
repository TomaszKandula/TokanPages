using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Handlers.Users
{

    public class UpdateUserCommandHandlerTest : TestBase
    {

        [Fact]
        public async Task UpdateUser_WhenIdIsCorrect_ShouldUpdateEntity()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = "ester1990@gmail.com",
                UserAlias = "Ester1990",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = "ester.exposito@gmail.com",
                UserAlias = "Ester",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            LDatabaseContext.Users.Add(LUser);
            LDatabaseContext.SaveChanges();

            // Act
            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext);
            await LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LUserEntity = LAssertDbContext.Users.Find(LUpdateUserCommand.Id);

            LUserEntity.Should().NotBeNull();
            LUserEntity.EmailAddress.Should().Be(LUpdateUserCommand.EmailAddress);
            LUserEntity.UserAlias.Should().Be(LUpdateUserCommand.UserAlias);
            LUserEntity.FirstName.Should().Be(LUpdateUserCommand.FirstName);
            LUserEntity.LastName.Should().Be(LUpdateUserCommand.LastName);
            LUserEntity.IsActivated.Should().BeTrue();
            LUserEntity.LastUpdated.Should().NotBeNull();

        }

        [Fact]
        public async Task UpdateUser_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.Parse("1edb4c7d-8cf0-4811-b721-af5caf74d7a8"),
                EmailAddress = "ester1990@gmail.com",
                UserAlias = "Ester1990",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = "ester.exposito@gmail.com",
                UserAlias = "Ester",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            LDatabaseContext.Users.Add(LUser);
            LDatabaseContext.SaveChanges();

            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None));

        }

        [Fact]
        public async Task UpdateUser_WhenEmailExists_ShouldThrowError()
        {

            // Arrange
            var LTestEmail = "ester1990@gmail.com";
            var LUpdateUserCommand = new UpdateUserCommand
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = LTestEmail,
                UserAlias = "Ester1990",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUser = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = LTestEmail,
                UserAlias = "Ester",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            LDatabaseContext.Users.Add(LUser);
            LDatabaseContext.SaveChanges();

            var LUpdateUserCommandHandler = new UpdateUserCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LUpdateUserCommandHandler.Handle(LUpdateUserCommand, CancellationToken.None));

        }

    }

}
