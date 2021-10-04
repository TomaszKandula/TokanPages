namespace TokanPages.Backend.Tests.Services
{
    using System.Collections.Generic;
    using Core.Utilities.TemplateService;
    using FluentAssertions;
    using Xunit;

    public class TemplateServiceTest
    {
        [Fact]
        public void GivenBodyTemplateAndItems_WhenInvokeMakeBody_ShouldReplaceTagsWithValues()
        {
            // Arrange
            const string BODY_TEMPLATE = "This is {VALUE1} test for {VALUE2}.";
            const string EXPECTED_CONTENT = "This is unit test for MakeBody method.";
            var LItems = new Dictionary<string, string>
            {
                { "{VALUE1}", "unit" },
                { "{VALUE2}", "MakeBody method" }
            };

            var LTemplateHelper = new TemplateService();
            
            // Act
            var LResult = LTemplateHelper.MakeBody(BODY_TEMPLATE, LItems);

            // Assert
            LResult.Should().Be(EXPECTED_CONTENT);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenEmptyBodyTemplateAndItems_WhenInvokeMakeBody_ShouldReturnNull(string ABodyTemplate)
        {
            // Arrange
            var LItems = new Dictionary<string, string>
            {
                { "{VALUE1}", "unit" },
                { "{VALUE2}", "MakeBody method" }
            };

            var LTemplateHelper = new TemplateService();
            
            // Act
            var LResult = LTemplateHelper.MakeBody(ABodyTemplate, LItems);

            // Assert
            LResult.Should().BeNull();
        }

        [Theory]
        [InlineData(null)]
        [MemberData(nameof(InputTestObject))]
        public void GivenBodyTemplateAndEmptyItems_WhenInvokeMakeBody_ShouldReturnNull(Dictionary<string, string> AItems)
        {
            // Arrange
            const string BODY_TEMPLATE = "This is {VALUE1} test for {VALUE2}.";
            var LTemplateHelper = new TemplateService();
            
            // Act
            var LResult = LTemplateHelper.MakeBody(BODY_TEMPLATE, AItems);

            // Assert
            LResult.Should().BeNull();
        }

        public static IEnumerable<object[]> InputTestObject()
        {
            return new List<object[]>
            {
                new object[] { new Dictionary<string, string>() }  
            };
        }
    }
}