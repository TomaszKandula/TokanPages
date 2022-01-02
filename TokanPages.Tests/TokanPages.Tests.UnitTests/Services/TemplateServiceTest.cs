namespace TokanPages.Tests.UnitTests.Services;

using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Backend.Core.Utilities.TemplateService;

public class TemplateServiceTest
{
    [Fact]
    public void GivenBodyTemplateAndItems_WhenInvokeMakeBody_ShouldReplaceTagsWithValues()
    {
        // Arrange
        const string bodyTemplate = "This is {VALUE1} test for {VALUE2}.";
        const string expectedContent = "This is unit test for MakeBody method.";
        var items = new Dictionary<string, string>
        {
            { "{VALUE1}", "unit" },
            { "{VALUE2}", "MakeBody method" }
        };

        var templateHelper = new TemplateService();
            
        // Act
        var result = templateHelper.MakeBody(bodyTemplate, items);

        // Assert
        result.Should().Be(expectedContent);
    }
        
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void GivenEmptyBodyTemplateAndItems_WhenInvokeMakeBody_ShouldReturnNull(string bodyTemplate)
    {
        // Arrange
        var items = new Dictionary<string, string>
        {
            { "{VALUE1}", "unit" },
            { "{VALUE2}", "MakeBody method" }
        };

        var templateHelper = new TemplateService();
            
        // Act
        var result = templateHelper.MakeBody(bodyTemplate, items);

        // Assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData(null)]
    [MemberData(nameof(InputTestObject))]
    public void GivenBodyTemplateAndEmptyItems_WhenInvokeMakeBody_ShouldReturnNull(Dictionary<string, string> items)
    {
        // Arrange
        const string bodyTemplate = "This is {VALUE1} test for {VALUE2}.";
        var templateHelper = new TemplateService();
            
        // Act
        var result = templateHelper.MakeBody(bodyTemplate, items);

        // Assert
        result.Should().BeNull();
    }

    public static IEnumerable<object[]> InputTestObject()
    {
        return new List<object[]>
        {
            new object[] { new Dictionary<string, string>() }  
        };
    }
}