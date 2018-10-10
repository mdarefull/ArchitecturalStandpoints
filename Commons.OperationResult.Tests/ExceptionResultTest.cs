using System;
using FluentAssertions;
using Xunit;

namespace Commons.OperationResult.Tests
{
    public class ExceptionResultTest
    {
        [Fact]
        public void ExceptionResult_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(ExceptionResult);
            var basicMembers = 5;
            var inheritedMembers = 3 * 3;
            var implementedMembers = 1 * 3;
            var totalMembers = basicMembers + inheritedMembers + implementedMembers;

            // Assert:
            type.Should().NotBeSealed()
                .And.BeDerivedFrom<FailureResult>()
                .And.HaveProperty<Exception>(nameof(ExceptionResult.InnerException));
            type.GetMembers().Should().HaveCount(totalMembers);
        }

        [Fact]
        public void ExceptionResultOfT_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(ExceptionResult<object>);
            var basicMembers = 5;
            var inheritedMembers = 3 * 3 + 1 * 3;
            var implementedMembers = 1;
            var totalMembers = basicMembers + inheritedMembers + implementedMembers;

            // Assert:
            type.Should().BeSealed()
                .And.BeDerivedFrom<ExceptionResult>()
                .And.Implement<IResult<object>>();
            type.IsGenericType.Should().BeTrue();
            type.GetMembers().Should().HaveCount(totalMembers);

        }
        [Fact]
        public void ConvertTo_NewValue_ReturnNewExceptionResultPresservingProperties()
        {
            // Arrange:
            var result = new ExceptionResult<int>
            {
                ErrorTitle = "Custom Title",
                ErrorDescription = "Custom Description",
                InnerException = new Exception(),
            };

            // Act:
            var newResult = result.ConvertTo<double>();

            // Assert:
            var failureResult = newResult.Should().BeOfType<ExceptionResult<double>>().Subject;
            failureResult.Should().NotBeSameAs(result);
            failureResult.Should().BeEquivalentTo(result);
        }
    }
}
