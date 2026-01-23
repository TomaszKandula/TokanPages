using FluentAssertions;
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
}
