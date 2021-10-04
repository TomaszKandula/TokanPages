namespace TokanPages.Backend.Tests.Validators.Content
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using Cqrs.Handlers.Queries.Content;

    public class GetContentQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenRequiredFields_WhenGetContent_ShouldFinishSuccessful()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Type = DataUtilityService.GetRandomString(),
                Name = DataUtilityService.GetRandomString()
            };

            // Act
            var LGetContentQueryValidator = new GetContentQueryValidator();
            var LResult = LGetContentQueryValidator.Validate(LGetContentQuery);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenMissingType_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Type = string.Empty,
                Name = DataUtilityService.GetRandomString()
            };

            // Act
            var LGetContentQueryValidator = new GetContentQueryValidator();
            var LResult = LGetContentQueryValidator.Validate(LGetContentQuery);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenMissingName_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Type = DataUtilityService.GetRandomString(),
                Name = string.Empty
            };

            // Act
            var LGetContentQueryValidator = new GetContentQueryValidator();
            var LResult = LGetContentQueryValidator.Validate(LGetContentQuery);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenMissingNameAndType_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var LGetContentQuery = new GetContentQuery
            {
                Type = string.Empty,
                Name = string.Empty
            };

            // Act
            var LGetContentQueryValidator = new GetContentQueryValidator();
            var LResult = LGetContentQueryValidator.Validate(LGetContentQuery);

            // Assert
            LResult.Errors.Count.Should().Be(2);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            LResult.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}