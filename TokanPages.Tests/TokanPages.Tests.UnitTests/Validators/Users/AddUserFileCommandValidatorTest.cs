using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class AddUserFileCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidFile_WhenInvokeValidation_ShouldSucceed()
    {
        // Arrange
        const int testFileSizeInKb = 1024;
        const string name = "Test";
        const string fileName = "TestFile.mov";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var formFile = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var maxFileSize = SetReturnValue("2048");
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeUserMedia"))
            .Returns(maxFileSize);

        var command = new AddUserFileCommand
        {
            Type = UserFileToUpdate.Video,
            BinaryData = formFile
        };

        // Act
        var validator = new AddUserFileCommandValidator(mockConfiguration.Object);
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenInvalidFileSize_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        const int testFileSizeInKb = 2048;
        const string name = "Test";
        const string fileName = "TestFile.mov";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var formFile = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var maxFileSize = SetReturnValue("1024");
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeUserMedia"))
            .Returns(maxFileSize);

        var command = new AddUserFileCommand
        {
            Type = UserFileToUpdate.Video,
            BinaryData = formFile
        };

        // Act
        var validator = new AddUserFileCommandValidator(mockConfiguration.Object);
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_FILE_SIZE));
    }

    [Fact]
    public async Task GivenInvalidFileType_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        const int testFileSizeInKb = 1048;
        const string name = "Test";
        const string fileName = "TestFile.mov";

        var stream = DataUtilityService.GetRandomStream(testFileSizeInKb);
        var formFile = new FormFile(stream, 0, testFileSizeInKb, name, fileName);

        var maxFileSize = SetReturnValue("2024");
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeUserMedia"))
            .Returns(maxFileSize);

        var command = new AddUserFileCommand
        {
            Type = (UserFileToUpdate)900,
            BinaryData = formFile
        };

        // Act
        var validator = new AddUserFileCommandValidator(mockConfiguration.Object);
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_ENUM_VALUE));
    }

    [Fact]
    public async Task GivenMissingFile_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        var maxFileSize = SetReturnValue("1048");
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeUserMedia"))
            .Returns(maxFileSize);

        var command = new AddUserFileCommand
        {
            Type = UserFileToUpdate.Video,
        };

        // Act
        var validator = new AddUserFileCommandValidator(mockConfiguration.Object);
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public async Task GivenEmptyCommand_WhenInvokeValidation_ShouldFail()
    {
        // Arrange
        var maxFileSize = SetReturnValue("1048");
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration
            .Setup(configuration => configuration.GetSection("AZ_Storage_MaxFileSizeUserMedia"))
            .Returns(maxFileSize);

        var command = new AddUserFileCommand();

        // Act
        var validator = new AddUserFileCommandValidator(mockConfiguration.Object);
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_ENUM_VALUE));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}