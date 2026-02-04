using FluentAssertions;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess;
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
    public void GivenEntityWithFilter_WhenGenerateQueryStatement_ShouldSucceed()
    {
        // Arrange
        var filterBy = new 
        {
            Name = "Victoria",
            IsPublished = true,
            CreatedAt = DateTime.Parse("2020-09-27"),
        };

        const string expectedValues = "Name=@Name AND IsPublished=@IsPublished AND CreatedAt=@CreatedAt";
        const string expectedStatement = $"SELECT Id,Name,IsPublished,CreatedAt,Likes FROM soccer.Players WHERE {expectedValues}";

        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateQueryStatement<TestPlayerOne>(filterBy);

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntityWithoutFilter_WhenGenerateQueryStatement_ShouldSucceed()
    {
        // Arrange
        const string expectedStatement = $"SELECT Id,Name,IsPublished,CreatedAt,Likes FROM soccer.Players";
        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateQueryStatement<TestPlayerOne>();

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntityWithOrderBy_WhenGenerateQueryStatement_ShouldSucceed()
    {
        // Arrange
        var orderBy = new
        {
            Likes = "DESC",
            Name = "ASC"
        };

        const string expectedStatement = $"SELECT Id,Name,IsPublished,CreatedAt,Likes FROM soccer.Players ORDER BY Likes DESC,Name ASC";
        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateQueryStatement<TestPlayerOne>(orderBy: orderBy);

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntityWithWhereAndOrderBy_WhenGenerateQueryStatement_ShouldSucceed()
    {
        // Arrange
        var filterBy = new 
        {
            Name = "Victoria",
            IsPublished = true,
            CreatedAt = DateTime.Parse("2020-09-27"),
        };

        var orderBy = new
        {
            Likes = "DESC",
            Name = "ASC"
        };

        const string expectedValues = "Name=@Name AND IsPublished=@IsPublished AND CreatedAt=@CreatedAt";
        const string expectedStatement = $"SELECT Id,Name,IsPublished,CreatedAt,Likes FROM soccer.Players WHERE {expectedValues} ORDER BY Likes DESC,Name ASC";
        var sqlGenerator = new SqlGenerator();

        // Act
        var result = sqlGenerator.GenerateQueryStatement<TestPlayerOne>(filterBy, orderBy);

        // Assert
        result.Should().Be(expectedStatement);
    }

    [Fact]
    public void GivenEntityAndInvalidFilter_WhenGenerateQueryStatement_ShouldFail()
    {
        // Arrange
        var filterBy = new 
        {
            SomeField = "Victoria"
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<ArgumentOutOfRangeException>(() => sqlGenerator.GenerateQueryStatement<TestPlayerOne>(filterBy));
        result.ParamName.Should().Be("SomeField");
        result.Message.Should().Contain(ErrorCodes.INVALID_COLUMN_NAME);
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

        const string expectedValues = "@Id,@Name,@IsPublished,@CreatedAt,@Likes";
        const string expectedStatement = $"INSERT INTO soccer.Players (Id,Name,IsPublished,CreatedAt,Likes) VALUES ({expectedValues})";

        var sqlGenerator = new SqlGenerator();

        // Act
        var (query, parameters) = sqlGenerator.GenerateInsertStatement(entity);

        // Assert
        query.Should().Be(expectedStatement);
        parameters.Should().NotBeNull();
    }

    [Fact]
    public void GivenManyEntities_WhenGenerateInsertStatement_ShouldSucceed()
    {
        // Arrange
        var entities = new List<TestPlayerOne>
        {
            new()
            {
                Id = Guid.Parse("c388e731-0e0f-4886-8326-a97769e51912"),
                Name = "Victoria",
                IsPublished = true,
                CreatedAt = DateTime.Parse("2020-09-27"),
                Likes = 2026
            },
            new()
            {
                Id = Guid.Parse("e5f9e867-7d54-4daa-9edd-882a3d26fe60"),
                Name = "Dawn",
                IsPublished = false,
                CreatedAt = DateTime.Parse("2026-09-07"),
                Likes = 1050
            }
        };

        const string expectedValues = "(@Id1,@Name1,@IsPublished1,@CreatedAt1,@Likes1),(@Id2,@Name2,@IsPublished2,@CreatedAt2,@Likes2)";
        const string expectedStatement = $"INSERT INTO soccer.Players (Id,Name,IsPublished,CreatedAt,Likes) VALUES {expectedValues}";

        var sqlGenerator = new SqlGenerator();

        // Act
        var (query, parameters) = sqlGenerator.GenerateInsertStatement(entities);

        // Assert
        query.Should().Be(expectedStatement);
        parameters.Should().NotBeNull();
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

        const string expectedValues = "Name=@Name WHERE Id=@Id";
        const string expectedStatement = $"UPDATE soccer.Players SET {expectedValues}";

        var sqlGenerator = new SqlGenerator();

        // Act
        var (query, parameters) = sqlGenerator.GenerateUpdateStatement<TestPlayerOne>(updateBy, filterBy);

        // Assert
        query.Should().Be(expectedStatement);
        parameters.Should().NotBeNull();
    }

    [Fact]
    public void GivenEntityAndInvalidFilter_WhenGenerateUpdateStatement_ShouldFail()
    {
        // Arrange
        var updateBy = new
        {
            Name = "Victoria",
            IsPublished = true
        };

        var filterBy = new
        {
            SomeField = "Victoria"
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<ArgumentOutOfRangeException>(() => sqlGenerator.GenerateUpdateStatement<TestPlayerOne>(updateBy, filterBy));
        result.ParamName.Should().Be("SomeField");
        result.Message.Should().Contain(ErrorCodes.INVALID_COLUMN_NAME);
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

        const string expectedValues = "Id=@Id AND Name=@Name AND CreatedAt=@CreatedAt";
        const string expectedStatement = $"DELETE FROM soccer.Players WHERE {expectedValues}";

        var sqlGenerator = new SqlGenerator();

        // Act
        var (query, parameters) = sqlGenerator.GenerateDeleteStatement<TestPlayerOne>(deleteBy);

        // Assert
        query.Should().Be(expectedStatement);
        parameters.Should().NotBeNull();
    }

    [Fact]
    public void GivenEntityAndInvalidFilter_WhenGenerateDeleteStatement_ShouldFail()
    {
        // Arrange
        var deleteBy = new
        {
            SomeField = "Victoria"
        };

        var sqlGenerator = new SqlGenerator();

        // Act
        // Assert
        var result = Assert.Throws<ArgumentOutOfRangeException>(() => sqlGenerator.GenerateDeleteStatement<TestPlayerOne>(deleteBy));
        result.ParamName.Should().Be("SomeField");
        result.Message.Should().Contain(ErrorCodes.INVALID_COLUMN_NAME);
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
