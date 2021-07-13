namespace TokanPages.Backend.Tests.Validators.Users
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Queries.Users;
    using FluentAssertions;
    using Xunit;

    public class GetUserQueryValidatorTest
    {
        [Fact]
        public void GivenCorrectId_WhenGetUser_ShouldFinishSuccessful()
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
        public void GivenEmptyId_WhenGetUser_ShouldThrowError()
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