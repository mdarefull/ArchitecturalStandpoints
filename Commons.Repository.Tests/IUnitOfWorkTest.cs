using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Data;
using System.Linq;

namespace Commons.Repository.Tests
{
    public class IUnitOfWorkTest
    {
        [Fact]
        public void IUnitOfWork_Is_WellDefined()
        {
            // Arrange
            var type = typeof(IUnitOfWork);
            var baseMembers = 2;
            var definedMembers = 5;
            var totalMembers = baseMembers + definedMembers;

            // Act

            // Assert
            type.IsInterface.Should().BeTrue();
            type.Should().Implement<IDisposable>()
                .And.HaveProperty<IDbConnection>(nameof(IUnitOfWork.Connection))
                .And.HaveProperty<IDbTransaction>(nameof(IUnitOfWork.Transaction))
                .And.HaveMethod(nameof(IUnitOfWork.BeginTransaction), new[] { typeof(IsolationLevel?) })
                .And.HaveMethod(nameof(IUnitOfWork.Commit), Enumerable.Empty<Type>())
                .And.HaveMethod(nameof(IUnitOfWork.Rollback), Enumerable.Empty<Type>());
            type.GetMembers().Should().HaveCount(totalMembers);
        }
    }
}
