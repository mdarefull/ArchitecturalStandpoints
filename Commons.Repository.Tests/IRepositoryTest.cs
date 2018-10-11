using System;
using System.Linq;

using FluentAssertions;

using Xunit;

namespace Commons.Repository.Tests
{
    public class IRepositoryTest
    {
        [Fact]
        public void IRepositoryOfTAndY_Is_WellDefined()
        {
            // Arrange
            var type = typeof(IRepository<object, int>);
            var totalMembers = 6;

            // Assert
            type.IsInterface.Should().BeTrue();
            type.IsGenericType.Should().BeTrue();
            type.GetMembers().Should().HaveCount(totalMembers);
            type.Should().HaveMethod(nameof(IRepository<object, int>.GetByIdAsync), new[] { typeof(int) })
                .And.HaveMethod(nameof(IRepository<object, int>.GetAllAsync), Enumerable.Empty<Type>())
                .And.HaveMethod(nameof(IRepository<object, int>.AddAsync), new[] { typeof(object) })
                .And.HaveMethod(nameof(IRepository<object, int>.UpdateAsync), new[] { typeof(object) })
                .And.HaveMethod(nameof(IRepository<object, int>.RemoveByIdAsync), new[] { typeof(int) })
                .And.HaveMethod(nameof(IRepository<object, int>.RemoveAsync), new[] { typeof(object) });
        }

        [Fact]
        public void IRepositoryOfT_Is_WellDefined()
        {
            // Arrange
            var type = typeof(IRepository<object>);
            var totalMembers = 0;

            // Assert
            type.IsInterface.Should().BeTrue();
            type.IsGenericType.Should().BeTrue();
            type.Should().Implement<IRepository<object, long>>();
            type.GetMembers().Should().HaveCount(totalMembers);
        }
    }
}
