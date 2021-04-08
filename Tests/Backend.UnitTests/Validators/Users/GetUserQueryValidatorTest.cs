using Xunit;
using FluentAssertions;
using System;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;

namespace Backend.UnitTests.Validators.Users
{
    public class GetUserQueryValidatorTest
    {
        [Fact]
        public void GetUser_WhenIdIsCorrect_ShouldFinishSuccessful()
        {
            // Arrange
            var LGetUserQuery = new GetUserQuery
            {
                Id = Guid.NewGuid()
            };

            // Act
            var LValidator = new GetUserQueryValidator();
            var LResult = LValidator.Validate(LGetUserQuery);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GetUser_WhenIdIsEmpty_ShouldThrowError()
        {
            // Arrange
            var LGetUserQuery = new GetUserQuery
            {
                Id = Guid.Empty
            };

            // Act
            var LValidator = new GetUserQueryValidator();
            var LResult = LValidator.Validate(LGetUserQuery);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}
