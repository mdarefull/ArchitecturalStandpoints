using FluentAssertions;
using Xunit;

namespace Commons.OperationResult.Tests
{
    public class FailureResultTest
    {
        [Fact]
        public void FailureResult_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(FailureResult);
            var basicMembers = 5;
            var propertyMembers = 3 * 3;
            var totalMembers = basicMembers + propertyMembers;

            // Assert:
            type.Should().Implement<IResult>()
                .And.NotBeSealed()
                .And.HaveProperty<string>(nameof(FailureResult.ErrorCode))
                .And.HaveProperty<string>(nameof(FailureResult.ErrorTitle))
                .And.HaveProperty<string>(nameof(FailureResult.ErrorDescription));
            type.GetMembers().Should().HaveCount(totalMembers);
        }

        [Fact]
        public void FailureResultOfT_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(FailureResult<object>);
            var basicMembers = 5;
            var inheritedMembers = 3 * 3;
            var implementedMembers = 1;
            var totalMembers = basicMembers + inheritedMembers + implementedMembers;

            // Assert:
            type.Should().Implement<IResult<object>>()
                .And.NotBeSealed()
                .And.BeDerivedFrom<FailureResult>();
            type.IsGenericType.Should().BeTrue();
            type.GetMembers().Should().HaveCount(totalMembers);
        }
        [Fact]
        public void ConvertTo_NewValue_ReturnNewFailureResultPresservingProperties()
        {
            // Arrange:
            var result = new FailureResult<int>
            {
                ErrorTitle = "Custom Title",
                ErrorDescription = "Custom Description",
            };

            // Act:
            var newResult = result.ConvertTo<double>();

            // Assert:
            var failureResult = newResult.Should().BeOfType<FailureResult<double>>().Subject;
            failureResult.Should().NotBeSameAs(result);
            failureResult.Should().BeEquivalentTo(result);
        }
    }
}
