using FluentAssertions;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Helpers;
using TokanPages.Tests.UnitTests.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.DataAccess;

public class SqlGeneratorTests : TestBase
{
    [Fact]
    public void GivenClassWithSchemaAndTable_WhenGetTableName_ShouldSucceed()
    {
        // Arrange
        const string expectedTableName = "soccer.Players";
        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GetTableName<TestPlayerOne>();

        // Assert
        result.Should().Be(expectedTableName);
    }

    [Fact]
    public void GivenClassWithSchemaOnly_WhenGetTableName_ShouldSucceed()
    {
        // Arrange
        const string expectedTableName = "soccer.TestPlayerTwo";
        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GetTableName<TestPlayerTwo>();

        // Assert
        result.Should().Be(expectedTableName);
    }

    [Fact]
    public void GivenClassWithTableNameOnly_WhenGetTableName_ShouldSucceed()
    {
        // Arrange
        const string expectedTableName = "dbo.Players";
        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GetTableName<TestPlayerThree>();

        // Assert
        result.Should().Be(expectedTableName);
    }

    [Fact]
    public void GivenClassWithoutSchemaAndTable_WhenGetTableName_ShouldSucceed()
    {
        // Arrange
        const string expectedTableName = "TestPlayerFour";
        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GetTableName<TestPlayerFour>();

        // Assert
        result.Should().Be(expectedTableName);
    }

    [Fact]
    public void GivenEntity_WhenGenerateInsertStatement_ShouldSucceed()
    {
        // Arrange
        var article = new TestPlayerOne
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            IsPublished = true,
            CreatedAt = DateTime.Parse("2020-09-27"),
            Likes = 2026
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateInsertStatement(article);

        // Assert
        result.Should().Be("INSERT INTO soccer.Players (Id,Name,IsPublished,CreatedAt,Likes) VALUES ('c388e731-0e0f-4886-8326-a97769e51912','Victoria',1,'27/09/2020 00:00:00',2026)");
    }

    [Fact]
    public void GivenEntity_WhenGenerateUpdateStatement_ShouldSucceed()
    {
        // Arrange
        var article = new TestPlayerOne
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            IsPublished = true,
            CreatedAt = DateTime.Parse("2020-09-27"),
            Likes = 2026
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateUpdateStatement(article);

        // Assert
        result.Should().Be("UPDATE soccer.Players SET Name='Victoria',IsPublished=1,CreatedAt='27/09/2020 00:00:00',Likes=2026 WHERE Id='c388e731-0e0f-4886-8326-a97769e51912'");
    }

    [Fact]
    public void GivenEntityWithoutPrimaryKey_WhenGenerateUpdateStatement_ShouldFail()
    {
        // Arrange
        var article = new TestPlayerTwo
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            IsPublished = true
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<GeneralException>(() => sqlGenerator.GenerateUpdateStatement(article));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ERROR_UNEXPECTED));
    }

    [Fact]
    public void GivenEntity_WhenGenerateDeleteStatement_ShouldSucceed()
    {
        // Arrange
        var article = new TestPlayerOne
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            IsPublished = true,
            CreatedAt = DateTime.Parse("2020-09-27"),
            Likes = 2026
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateDeleteStatement(article);

        // Assert
        result.Should().Be("DELETE FROM soccer.Players WHERE Id='c388e731-0e0f-4886-8326-a97769e51912' AND Name='Victoria' AND IsPublished=1 AND CreatedAt='27/09/2020 00:00:00' AND Likes=2026");
    }
}
