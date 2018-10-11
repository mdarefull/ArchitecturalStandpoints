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
        public void EqualsOfObject_ObjIsNotEntity_False()
        {
            // Arrange
            var subject = new Mock<EntityBase<object>>
            {
                CallBase = true,
            }.Object;
            var other = 0;

            // Act
            var result = subject.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsOfObject_ObjIsIEntity_InvokesEqualsOfEntity()
        {
            // Arrange
            var subjectMock = new Mock<EntityBase<object>>
            {
                CallBase = true,
            };
            subjectMock
            .Setup(s => s.Equals(It.IsAny<EntityBase<object>>()))
            .Returns(true);
            var subject = subjectMock.Object;

            var other = Mock.Of<EntityBase<object>>();

            // Act
            subject.Equals(other as object);

            // Assert
            subjectMock.Verify(s => s.Equals(It.IsAny<EntityBase<object>>()), Times.Once);
        }

        [Fact]
        public void GetHashCode_Invoke_ReturnsIdGetHashCode()
        {
            // Arrange
            var expectedHashCode = 12345;
            var idStub = Mock.Of<object>(s => s.GetHashCode() == expectedHashCode);
            var subject = new Mock<EntityBase<object>>
            {
                CallBase = true
            }
            .SetupProperty(s => s.Id, idStub)
            .Object;

            // Act
            var hashCode = subject.GetHashCode();

            // Assert
            hashCode.Should().Be(expectedHashCode);
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
