using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Commons.OperationResult.Tests
{
    public class SuccessResultTest
    {
        [Fact]
        public void SuccessResult_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(SuccessResult);
            var basicMembers = 5;

            // Assert:
            type.Should().Implement<IResult>()
                .And.Subject.GetMembers().Should().HaveCount(basicMembers);
        }

        [Fact]
        public void SuccessResultOfT_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(SuccessResult<object>);
            var basicMembers = 5;
            var implementedMembers = 1;
            var valuePropertyMembers = 1 * 3;
            var totalMembers = basicMembers + implementedMembers + valuePropertyMembers;

            // Assert:
            type.Should().BeDerivedFrom<SuccessResult>()
                .And.Implement<IResult<object>>()
                .And.BeSealed()
                .And.HaveProperty<object>(nameof(SuccessResult<object>.Value));
            type.IsGenericType.Should().BeTrue();
            type.GetMembers().Should().HaveCount(totalMembers);
        }
        [Fact]
        public void ConvertTo_NewValue_ReturnNewSuccessResultWithNewValue()
        {
            // Arrange:
            var oldValue = 5;
            var result = new SuccessResult<int> { Value = oldValue };
            var newValue = oldValue + 1.5;

            // Act:
            var newResult = result.ConvertTo(newValue);

            // Assert:
            var successResult = newResult.Should().BeOfType<SuccessResult<double>>().Subject;
            successResult.Should().NotBeSameAs(result);
            successResult.Value.Should().Be(newValue);
        }
    }
}
