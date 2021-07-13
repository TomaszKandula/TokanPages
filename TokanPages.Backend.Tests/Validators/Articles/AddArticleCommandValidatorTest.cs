namespace TokanPages.Backend.Tests.Validators.Articles
{
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Articles;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;

    public class AddArticleCommandValidatorTest
    {
        private readonly DataUtilityService FDataUtilityService;

        public AddArticleCommandValidatorTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenValidateAddArticle_ShouldFinishSuccessfully() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand 
            { 
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenDescriptionTooLong_WhenValidateAddArticle_ShouldThrowError() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(256),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
        }

        [Fact]
        public void GivenTitleTooLong_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataUtilityService.GetRandomString(256),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
        }

        [Fact]
        public void GivenDescriptionEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataUtilityService.GetRandomString(),
                Description = string.Empty,
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTitleEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = string.Empty,
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTextToUploadEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenImageToUploadEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = string.Empty
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}