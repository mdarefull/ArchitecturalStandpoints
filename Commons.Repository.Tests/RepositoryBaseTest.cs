using FluentAssertions;

using Xunit;

namespace Commons.Repository.Tests
{
    public class RepositoryBaseTest
    {
        [Fact]
        public void RepositoryBaseOfTAndY_Is_WellDefined()
        {
            // Arrange
            var type = typeof(RepositoryBase<object, int>);
            var baseMembers = 4;
            var implementedMembers = 6;
            var constructorMember = 1;
            var totalMembers = baseMembers + implementedMembers + constructorMember;

            // Assert
            type.IsAbstract.Should().BeTrue();
            type.IsClass.Should().BeTrue();
            type.IsGenericType.Should().BeTrue();
            type.Should().Implement<IRepository<object, int>>()
                .And.HaveConstructor(new[] { typeof(UnitOfWork) });
            type.GetMembers().Should().HaveCount(totalMembers);
        }

        [Fact]
        public void RepositoryBaseOfT_Is_WellDefined()
        {
            // Arrange
            var type = typeof(RepositoryBase<object>);
            var inheritedMembers = 4 + 6 + 1;
            var totalMembers = inheritedMembers;

            // Act

            // Assert
            type.IsAbstract.Should().BeTrue();
            type.IsClass.Should().BeTrue();
            type.IsGenericType.Should().BeTrue();
            type.Should().BeDerivedFrom<RepositoryBase<object, long>>()
                .And.Implement<IRepository<object>>();
            type.GetMembers().Should().HaveCount(totalMembers);
        }
    }
}
