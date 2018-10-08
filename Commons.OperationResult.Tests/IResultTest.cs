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
            Assert.Empty(members);
        }

        [Fact]
        public void IResultOfT_Is_Generic()
        {
            // Arrange:
            var type = typeof(IResult<>);

            // Assert:
            Assert.True(type.IsGenericType);
        }

        [Fact]
        public void IResultOfT_Has_NMembers()
        {
            // Arrange:
            var type = typeof(IResult<>);

            // Act:
            var members = type.GetMembers();

            // Assert:
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
            Assert.NotNull(method);
            Assert.True(method.IsGenericMethod);
            Assert.Single(method.GetGenericArguments());

            var returnType = method.ReturnType;
            Assert.Contains(nameof(IResult), returnType.Name);
            Assert.True(returnType.IsGenericType);

            var parameters = method.GetParameters();
            Assert.Single(parameters);
            Assert.True(parameters[0].IsOptional);
            Assert.True(parameters[0].ParameterType.IsGenericParameter);
        }
    }
}
