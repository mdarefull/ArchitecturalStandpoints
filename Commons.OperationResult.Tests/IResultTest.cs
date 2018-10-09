using System.Linq;

using FluentAssertions;

using Xunit;

namespace Commons.OperationResult.Tests
{
    public class IResultTest
    {
        [Fact]
        public void IResult_Has_NoMembers()
        {
            // Arrange:
            var type = typeof(IResult);

            // Act:
            var members = type.GetMembers();

            // Assert:
            members.Should().BeEmpty();
        }

        [Fact]
        public void IResultOfT_Is_Generic()
        {
            // Arrange:
            var type = typeof(IResult<>);

            // Assert:
            type.IsGenericType.Should().BeTrue();
        }

        [Fact]
        public void IResultOfT_Has_NMembers()
        {
            // Arrange:
            var type = typeof(IResult<>);
            var n = 1;

            // Act:
            var members = type.GetMembers();

            // Assert:
            members.Should().HaveCount(n);
            Assert.Single(members);
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
