using FluentAssertions;
using Moq;
using Xunit;

namespace Commons.Repository.Tests
{
    public class EntityBaseTest
    {
        [Fact]
        public void EntityBaseOfT_Is_WellDefined()
        {
            // Arrange
            var type = typeof(EntityBase<object>);
            var baseMembers = 2;
            var overridenMembers = 2;
            var propertyMembers = 1 * 3;
            var definedMembers = 1;
            var totalMembers = baseMembers + overridenMembers + propertyMembers + definedMembers;

            // Assert
            type.IsAbstract.Should().BeTrue();
            type.IsClass.Should().BeTrue();
            type.IsGenericType.Should().BeTrue();

            type.Should().Implement<IEntity<object>>();
            type.GetMembers().Should().HaveCount(totalMembers);
        }

        [Fact]
        public void EqualsOfEntity_IdEqualsFalse_False()
        {
            // Arrange
            var idStub = Mock.Of<object>(s => s.Equals(It.IsAny<object>()) == false);
            var subjectMock = new Mock<EntityBase<object>>
            {
                CallBase = true
            };
            subjectMock.SetupProperty(s => s.Id, idStub);
            var subject = subjectMock.Object;

            var other = Mock.Of<IEntity<object>>();

            // Act
            var result = subject.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void EqualsOfEntity_IdEqualsTrueAndDifferentClasses_False()
        {
            // Arrange
            var idStub = Mock.Of<object>(s => s.Equals(It.IsAny<object>()) == true);
            var subjectMock = new Mock<EntityBase<object>>
            {
                CallBase = true
            };
            subjectMock.SetupProperty(s => s.Id, idStub);
            var subject = subjectMock.Object;

            var other = Mock.Of<IEntity<object>>();

            // Act
            var result = subject.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void EqualsOfEntity_IdEqualsTrueAndSameClassesDistincType_False()
        {
            // Arrange
            var idStub = Mock.Of<object>(s => s.Equals(It.IsAny<object>()) == true);
            var subjectMock = new Mock<EntityBase<object>>
            {
                CallBase = true
            };
            subjectMock.SetupProperty(s => s.Id, idStub);
            var subject = subjectMock.Object;

            var other = Mock.Of<EntityBase<int>>();

            // Act
            var result = subject.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void EqualsOfEntity_IdEqualsTrueAndSameClasses_True()
        {
            // Arrange
            var idStub = Mock.Of<object>(s => s.Equals(It.IsAny<object>()) == true);
            var subjectMock = new Mock<EntityBase<object>>
            {
                CallBase = true
            };
            subjectMock.SetupProperty(s => s.Id, idStub);
            var subject = subjectMock.Object;

            var other = Mock.Of<EntityBase<object>>();

            // Act
            var result = subject.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EqualsOfObject_ObjIsIEntity_InvokesEqualsOfEntity()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void EqualsOfObject_ObjIsNotEntity_False()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void GetHashCode_Invoke_ReturnsIdGetHashCode()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void EntityBase_Is_WellDefined()
        {
            // Arrange
            var type = typeof(EntityBase);
            var baseMembers = 2 + 2 + 1 * 3 + 1;
            var definedMembers = 0;
            var totalMembers = baseMembers + definedMembers;

            // Assert
            type.IsClass.Should().BeTrue();
            type.IsAbstract.Should().BeTrue();
            type.Should().BeDerivedFrom<EntityBase<long>>()
                .And.Implement<IEntity>();
            type.GetMembers().Should().HaveCount(totalMembers);
        }
    }
}
