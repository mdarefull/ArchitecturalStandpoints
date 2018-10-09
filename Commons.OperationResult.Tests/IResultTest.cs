using System.Linq;

using FluentAssertions;

using Xunit;

namespace Commons.OperationResult.Tests
{
    public class IResultTest
    {
        [Fact]
        public void IResult_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(IResult);

            // Assert:
            type.IsInterface.Should().BeTrue();
            type.GetMembers().Should().BeEmpty();
        }

        [Fact]
        public void IResultOfT_Is_WellDefined()
        {
            // Arrange:
            var type = typeof(IResult<object>);
            var members = 1;

            // Assert:
            type.IsInterface.Should().BeTrue();
            type.IsGenericType.Should().BeTrue();
            type.GetMembers().Should().HaveCount(members);
        }
        [Fact]
        public void IResultOfT_Defines_ConvertTo()
        {
            // Arrange:
            var type = typeof(IResult<>);
            var methodName = nameof(IResult<object>.ConvertTo);

            // Act:
            var method = type.GetMethod(methodName);

            // Assert:
            method.Should().NotBeNull();
            method.IsGenericMethod.Should().BeTrue();
            method.GetGenericArguments().Should().ContainSingle();

            var returnType = method.ReturnType;
            returnType.Name.Should().StartWith(nameof(IResult));
            returnType.IsGenericType.Should().BeTrue();

            var parameters = method.GetParameters();
            parameters.Should().ContainSingle();

            var parameter = parameters.First();
            parameter.IsOptional.Should().BeTrue();
            parameter.ParameterType.IsGenericParameter.Should().BeTrue();
        }
    }
}
