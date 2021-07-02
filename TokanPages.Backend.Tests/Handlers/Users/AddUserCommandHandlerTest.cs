using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Services.CipheringService;
using TokanPages.Backend.Core.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Handlers.Users
{   
    public class AddUserCommandHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public AddUserCommandHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenFieldsAreProvided_WhenAddUser_ShouldAddEntity() 
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand 
            {
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                Password = FDataProviderService.GetRandomString()
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
            var LTestEmail = FDataProviderService.GetRandomEmail();
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = LTestEmail,
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                EmailAddress = LTestEmail,
                IsActivated = false,
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataProviderService.GetRandomString()
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

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None));
        }
    }
}
