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
    public void GivenEntity_WhenGenerateQueryStatement_ShouldSucceed()
    {
        // Arrange
        var filterBy = new 
        {
            Name = "Victoria",
            IsPublished= true,
            CreatedAt= DateTime.Parse("2020-09-27"),
        };

        const string expectedValues = "Name='Victoria' AND IsPublished=1 AND CreatedAt='2020-09-27 00:00:00'";
        const string expectedStatement = $"SELECT Id,Name,IsPublished,CreatedAt,Likes FROM soccer.Players WHERE {expectedValues}";

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateQueryStatement<TestPlayerOne>(filterBy);

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntity_WhenGenerateInsertStatement_ShouldSucceed()
    {
        // Arrange
        var entity = new TestPlayerOne
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            IsPublished = true,
            CreatedAt = DateTime.Parse("2020-09-27"),
            Likes = 2026
        };

        const string expectedValues = "'c388e731-0e0f-4886-8326-a97769e51912','Victoria',1,'2020-09-27 00:00:00',2026";
        const string expectedStatement = $"INSERT INTO soccer.Players (Id,Name,IsPublished,CreatedAt,Likes) VALUES ({expectedValues})";

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateInsertStatement(entity);

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntityWithoutPrimaryKey_WhenGenerateInsertStatement_ShouldFail()
    {
        // Arrange
        var entity = new TestPlayerTwo
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            IsPublished = true
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<GeneralException>(() => sqlGenerator.GenerateInsertStatement(entity));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_PRIMARYKEY));
    }

    [Fact]
    public void GivenEntity_WhenGenerateUpdateStatement_ShouldSucceed()
    {
        // Arrange
        var updateBy = new
        {
            Name = "Victoria"
        };

        var filterBy = new
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912")
        };

        const string expectedValues = "Name='Victoria' WHERE Id='c388e731-0e0f-4886-8326-a97769e51912'";
        const string expectedStatement = $"UPDATE soccer.Players SET {expectedValues}";

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateUpdateStatement<TestPlayerOne>(updateBy, filterBy);

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntityWithoutFilterObject_WhenGenerateUpdateStatement_ShouldFail()
    {
        // Arrange
        var updateBy = new
        {
            Name = "Victoria",
            IsPublished = true
        };

        var filterBy = new { };
        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<GeneralException>(() => sqlGenerator.GenerateUpdateStatement<TestPlayerOne>(updateBy, filterBy));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_WHERE_CLAUSE));
    }

    [Fact]
    public void GivenEntityWithoutPrimaryKey_WhenGenerateUpdateStatement_ShouldFail()
    {
        // Arrange
        var updateBy = new
        {
            Name = "Victoria",
            IsPublished = true
        };

        var filterBy = new
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912")
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<GeneralException>(() => sqlGenerator.GenerateUpdateStatement<TestPlayerTwo>(updateBy, filterBy));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_PRIMARYKEY));
    }

    [Fact]
    public void GivenEntity_WhenGenerateDeleteStatement_ShouldSucceed()
    {
        // Arrange
        var deleteBy = new
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            CreatedAt = DateTime.Parse("2020-09-27"),
        };

        const string expectedValues = "Id='c388e731-0e0f-4886-8326-a97769e51912' AND Name='Victoria' AND CreatedAt='2020-09-27 00:00:00'";
        const string expectedStatement = $"DELETE FROM soccer.Players WHERE {expectedValues}";

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateDeleteStatement<TestPlayerOne>(deleteBy);

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntityWithoutDeleteObject_WhenGenerateDeleteStatement_ShouldFail()
    {
        // Arrange
        var deleteBy = new { };
        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<GeneralException>(() => sqlGenerator.GenerateDeleteStatement<TestPlayerOne>(deleteBy));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_WHERE_CLAUSE));
    }

    [Fact]
    public void GivenEntityWithoutPrimaryKey_WhenGenerateDeleteStatement_ShouldFail()
    {
        // Arrange
        var deleteBy = new 
        {
            Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
            Name = "Victoria",
            IsPublished = true
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<GeneralException>(() => sqlGenerator.GenerateDeleteStatement<TestPlayerTwo>(deleteBy));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_PRIMARYKEY));
    }
}
