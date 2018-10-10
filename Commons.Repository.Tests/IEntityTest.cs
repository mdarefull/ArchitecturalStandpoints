using System;

using FluentAssertions;

using Xunit;

namespace Commons.Repository.Tests
{
    public class IEntityTest
    {
        [Fact]
        public void IEntityOfT_Is_WellDefined()
        {
            // Arrange
            var type = typeof(IEntity<object>);
            var definedMembers = 1 * 3;
            var totalMembers = definedMembers;

            // Assert
            type.IsInterface.Should().BeTrue();
            type.IsGenericType.Should().BeTrue();
            type.Should().Implement<IEquatable<IEntity<object>>>()
                .And.HaveProperty<object>(nameof(IEntity<object>.Id));
            type.GetMembers().Should().HaveCount(totalMembers);
        }

        [Fact]
        public void IEntity_Is_WellDefined()
        {
            // Arrange
            var type = typeof(IEntity);
            var totalMembers = 0;

            // Assert
            type.Should().Implement<IEntity<long>>();
            type.IsInterface.Should().BeTrue();
            type.IsGenericType.Should().BeFalse();
            type.GetMembers().Should().HaveCount(totalMembers);
        }
    }
}
