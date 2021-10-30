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
            var getUserQuery = new GetUserQuery
            {
                Id = Guid.NewGuid()
            };

            // Act
            var validator = new GetUserQueryValidator();
            var result = validator.Validate(getUserQuery);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenGetUser_ShouldThrowError()
        {
            // Arrange
            var getUserQuery = new GetUserQuery
            {
                Id = Guid.Empty
            };

            // Act
            var validator = new GetUserQueryValidator();
            var result = validator.Validate(getUserQuery);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}