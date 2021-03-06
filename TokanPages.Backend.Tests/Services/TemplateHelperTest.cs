using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Services.TemplateHelper;

namespace TokanPages.Backend.Tests.Services
{
    public class TemplateHelperTest
    {
        [Fact]
        public void GivenBodyTemplateAndItems_WhenInvokeMakeBody_ShouldReplaceTagsWithValues()
        {
            // Arrange
            const string BODY_TEMPLATE = "This is {VALUE1} test for {VALUE2}.";
            const string EXPECTED_CONTENT = "This is unit test for MakeBody method.";
            var LItems = new List<TemplateItemModel>
            {
                new ()
                {
                    Tag = "{VALUE1}",
                    Value = "unit"
                },
                new ()
                {
                    Tag = "{VALUE2}",
                    Value = "MakeBody method"
                }
            };

            var LTemplateHelper = new TemplateHelper();
            
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
            var LItems = new List<TemplateItemModel>
            {
                new ()
                {
                    Tag = "{VALUE1}",
                    Value = "unit"
                },
                new ()
                {
                    Tag = "{VALUE2}",
                    Value = "MakeBody method"
                }
            };

            var LTemplateHelper = new TemplateHelper();
            
            // Act
            var LResult = LTemplateHelper.MakeBody(ABodyTemplate, LItems);

            // Assert
            LResult.Should().BeNull();
        }

        [Theory]
        [InlineData(null)]
        [MemberData(nameof(InputTestObject))]
        public void GivenBodyTemplateAndEmptyItems_WhenInvokeMakeBody_ShouldReturnNull(List<TemplateItemModel> AItems)
        {
            // Arrange
            const string BODY_TEMPLATE = "This is {VALUE1} test for {VALUE2}.";
            var LTemplateHelper = new TemplateHelper();
            
            // Act
            var LResult = LTemplateHelper.MakeBody(BODY_TEMPLATE, AItems);

            // Assert
            LResult.Should().BeNull();
        }

        public static IEnumerable<object[]> InputTestObject()
        {
            return new List<object[]>
            {
                new object[] { new List<TemplateItemModel>() }  
            };
        }
    }
}