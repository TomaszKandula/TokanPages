﻿namespace TokanPages.Backend.Tests.Handlers.Users
{   
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Commands.Users;
    using Cqrs.Services.CipheringService;
    using Shared.Services.DateTimeService;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class AddUserCommandHandlerTest : TestBase
    {
        private readonly DataUtilityService FDataUtilityService;

        public AddUserCommandHandlerTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public async Task GivenFieldsAreProvided_WhenAddUser_ShouldAddEntity() 
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand 
            {
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                Password = FDataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedCipher = new Mock<ICipheringService>();

            LMockedCipher
                .Setup(ACipher => ACipher.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("MockedPassword");
            
            var LAddUserCommandHandler = new AddUserCommandHandler(LDatabaseContext, LMockedDateTime.Object, LMockedCipher.Object);

            // Act
            await LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None);

            // Assert
            var LResult = LDatabaseContext.Users.ToList();

            LResult.Should().NotBeNull();
            LResult.Should().HaveCount(1);
            LResult[0].EmailAddress.Should().Be(LAddUserCommand.EmailAddress);
            LResult[0].UserAlias.Should().Be(LAddUserCommand.UserAlias);
            LResult[0].FirstName.Should().Be(LAddUserCommand.FirstName);
            LResult[0].LastName.Should().Be(LAddUserCommand.LastName);
            LResult[0].IsActivated.Should().BeTrue();
            LResult[0].LastLogged.Should().BeNull();
            LResult[0].LastUpdated.Should().BeNull();
        }

        [Fact]
        public async Task GivenExistingEmail_WhenAddUser_ShouldThrowError()
        {
            // Arrange
            var LTestEmail = FDataUtilityService.GetRandomEmail();
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = LTestEmail,
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                EmailAddress = LTestEmail,
                IsActivated = false,
                UserAlias = FDataUtilityService.GetRandomString(),
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTime = new Mock<DateTimeService>();
            var LMockedCipher = new Mock<ICipheringService>();
            
            LMockedCipher
                .Setup(ACipher => ACipher.GetHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("MockedPassword");
            
            var LAddUserCommandHandler = new AddUserCommandHandler(LDatabaseContext, LMockedDateTime.Object, LMockedCipher.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None));
        }
    }
}