using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Data;

namespace Commons.Repository.Tests
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void UnitOfWork_Is_WellDefined()
        {
            // Arrange
            var type = typeof(UnitOfWork);
            var inheritedMembers = 4;
            var propertyMembers = 2 * 2;
            var disposableMembers = 1;
            var methodMembers = 3;
            var constructorMembers = 1;
            var totalMembers = inheritedMembers + propertyMembers + disposableMembers + methodMembers + constructorMembers;

            // Assert
            type.IsClass.Should().BeTrue();
            type.Should().Implement<IUnitOfWork>()
                .And.HaveConstructor(new[] { typeof(IDbConnection) });
            type.GetMembers().Should().HaveCount(totalMembers);
        }

        // BeginTransaction:
        //  If already succeeded => ResultException with InvalidOperationException with message "There's a transaction already in progress."

    }
}
