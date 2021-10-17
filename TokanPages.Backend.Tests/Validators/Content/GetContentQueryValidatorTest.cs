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
            var getContentQuery = new GetContentQuery
            {
                Type = DataUtilityService.GetRandomString(),
                Name = DataUtilityService.GetRandomString()
            };

            // Act
            var getContentQueryValidator = new GetContentQueryValidator();
            var result = getContentQueryValidator.Validate(getContentQuery);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenMissingType_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Type = string.Empty,
                Name = DataUtilityService.GetRandomString()
            };

            // Act
            var getContentQueryValidator = new GetContentQueryValidator();
            var result = getContentQueryValidator.Validate(getContentQuery);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenMissingName_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Type = DataUtilityService.GetRandomString(),
                Name = string.Empty
            };

            // Act
            var getContentQueryValidator = new GetContentQueryValidator();
            var result = getContentQueryValidator.Validate(getContentQuery);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenMissingNameAndType_WhenGetContent_ShouldThrowError()
        {
            // Arrange
            var getContentQuery = new GetContentQuery
            {
                Type = string.Empty,
                Name = string.Empty
            };

            // Act
            var getContentQueryValidator = new GetContentQueryValidator();
            var result = getContentQueryValidator.Validate(getContentQuery);

            // Assert
            result.Errors.Count.Should().Be(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}