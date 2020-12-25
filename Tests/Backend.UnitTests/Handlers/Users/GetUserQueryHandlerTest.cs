using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;

namespace Backend.UnitTests.Handlers.Users
{

    public class GetUserQueryHandlerTest : TestBase
    {

        [Fact]
        public async Task GetUser_WhenIdIsCorrect_ShouldReturnEntity() 
        {

            // Arrange
            var LGetUserQuery = new GetUserQuery
            {
                Id = Guid.Parse("f985772e-4207-41f6-a5f3-d2c2b52d4033")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LTestDate = DateTime.Now;
            var LUsers = new TokanPages.Backend.Domain.Entities.Users 
            { 
                Id = Guid.Parse("f985772e-4207-41f6-a5f3-d2c2b52d4033"),
                EmailAddress = "ester1990@gmail.com",
                UserAlias = "ester1990",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
                Registered = LTestDate,
                LastUpdated = null,
                LastLogged = null
            };

            LDatabaseContext.Users.Add(LUsers);
            LDatabaseContext.SaveChanges();

            var LGetUserQueryHandler = new GetUserQueryHandler(LDatabaseContext);

            // Act
            var LResult = await LGetUserQueryHandler.Handle(LGetUserQuery, CancellationToken.None);

            // Assert
            LResult.Should().NotBeNull();
            LResult.EmailAddress.Should().Be(LUsers.EmailAddress);
            LResult.UserAlias.Should().Be(LUsers.UserAlias);
            LResult.FirstName.Should().Be(LUsers.FirstName);
            LResult.LastName.Should().Be(LUsers.LastName);
            LResult.IsActivated.Should().BeTrue();
            LResult.Registered.Should().Be(LTestDate);
            LResult.LastUpdated.Should().BeNull();
            LResult.LastLogged.Should().BeNull();

        }

        [Fact]
        public async Task GetUser_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LGetUserQuery = new GetUserQuery
            {
                Id = Guid.Parse("8f4cef66-6f37-49bb-bbe7-db6c54336d76")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = Guid.Parse("f985772e-4207-41f6-a5f3-d2c2b52d4033"),
                EmailAddress = "ester1990@gmail.com",
                UserAlias = "ester1990",
                FirstName = "Ester",
                LastName = "Exposito",
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };

            LDatabaseContext.Users.Add(LUsers);
            LDatabaseContext.SaveChanges();

            var LGetUserQueryHandler = new GetUserQueryHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LGetUserQueryHandler.Handle(LGetUserQuery, CancellationToken.None));

        }

    }

}
