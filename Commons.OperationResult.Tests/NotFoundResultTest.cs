using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Commons.OperationResult.Tests
{
    public class NotFoundResultTest
    {
        [Fact]
        public void NotFoundResult_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(NotFoundResult<object>);
            var basicMembers = 5;
            var inheritedMembers = 3 * 3 + 1;
            var implementedMembers = 0;
            var totalMembers = basicMembers + inheritedMembers + implementedMembers;

            // Assert:
            type.Should().BeSealed()
                .And.BeDerivedFrom<FailureResult<object>>();
            type.IsGenericType.Should().BeTrue();
            type.GetMembers().Should().HaveCount(totalMembers);
        }
        [Fact]
        public void ConvertTo_NewValue_ReturnNewFailureResultPresservingProperties()
        {
            // Arrange:
            var result = new NotFoundResult<int>
            {
                ErrorTitle = "Custom Title",
                ErrorDescription = "Custom Description",
            };

            // Act:
            var newResult = result.ConvertTo<double>();

            // Assert:
            var failureResult = newResult.Should().BeOfType<NotFoundResult<double>>().Subject;
            failureResult.Should().NotBeSameAs(result);
            failureResult.Should().BeEquivalentTo(result);
        }
    }
}
