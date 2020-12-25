using Xunit;
using FluentAssertions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Handlers.Users
{
    
    public class AddUserCommandHandlerTest : TestBase
    {

        [Fact]
        public async Task AddUser_WhenFieldsAreProvided_ShouldAddEntity() 
        {

            // Arrange
            var LAddUserCommand = new AddUserCommand 
            { 
                EmailAddress = "tokan@dfds.com",
                UserAlias = "tokan",
                FirstName = "Tomasz",
                LastName = "Tomasz"
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LAddUserCommandHandler = new AddUserCommandHandler(LDatabaseContext);

            // Act
            await LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LResult = LAssertDbContext.Users.ToList();

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

    }

}
