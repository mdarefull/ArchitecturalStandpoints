using System;
using System.Data;

using Commons.OperationResult;

using FluentAssertions;

using Moq;

using Xunit;

namespace Commons.Repository.Tests
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void UnitOfWork_Is_WellDefined()
        {
            // Arrange
            var type = typeof(UnitOfWork);
            var baseMembers = 4;
            var disposableMembers = 1;
            var methodMembers = 3;
            var constructorMembers = 1;
            var totalMembers = baseMembers + disposableMembers + methodMembers + constructorMembers;

            // Assert
            type.IsClass.Should().BeTrue();
            type.Should().Implement<IUnitOfWork>()
                .And.HaveConstructor(new[] { typeof(IDbConnection) });
            type.GetMembers().Should().HaveCount(totalMembers);
        }

        [Fact]
        public void BeginTransaction_AlreadyInProgress_ExceptionResult()
        {
            // Arrange
            var dbTransaction = Mock.Of<IDbTransaction>();
            var dbConnection = Mock.Of<IDbConnection>(
                m => m.State == ConnectionState.Closed
                  && m.BeginTransaction() == dbTransaction);
            var subject = new UnitOfWork(dbConnection);
            subject.BeginTransaction();

            // Act
            var result = subject.BeginTransaction();

            // Assert
            result.Should().BeOfType<ExceptionResult>()
                  .Which.InnerException.Should().BeOfType<InvalidOperationException>()
                  .Which.Message.Should().Contain("There's a transaction already in progress");
        }
        [Fact]
        public void BeginTransaction_ConnectionClosed_OpenIt()
        {
            // Arrange
            var dbConnection = Mock.Of<IDbConnection>(m => m.State == ConnectionState.Closed);
            var subject = new UnitOfWork(dbConnection);

            // Act
            subject.BeginTransaction();

            // Assert
            Mock.Get(dbConnection).Verify(m => m.Open(), Times.Once);
        }
        [Fact]
        public void BeginTransaction_OpenConnectionThrowsException_ReturnExceptionResult()
        {
            // Arrange
            var thrownException = new Exception();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Closed);
            dbConnectionMock.Setup(m => m.Open()).Throws(thrownException);

            var subject = new UnitOfWork(dbConnectionMock.Object);

            // Act
            var result = subject.BeginTransaction();

            // Assert
            var exceptionResult = result.Should().BeOfType<ExceptionResult>().Subject;
            exceptionResult.InnerException.Should().Be(thrownException);
            exceptionResult.ErrorCode.Should().Contain("Error opening the connection");
        }
        [Fact]
        public void BeginTransaction_NoIsolationLevel_InvokesConnectionBeginTransactionWithNoArgs()
        {
            // Arrange
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            var subject = new UnitOfWork(dbConnectionMock.Object);

            // Act
            subject.BeginTransaction();

            // Assert
            dbConnectionMock.Verify(m => m.BeginTransaction(), Times.Once);
            dbConnectionMock.Verify(m => m.BeginTransaction(It.IsAny<IsolationLevel>()), Times.Never);
        }
        [Fact]
        public void BeginTransaction_IsolationLevel_InvokesConnectionBeginTransactionWithIsolationLevel()
        {
            // Arrange
            var isolationLevel = IsolationLevel.ReadUncommitted;
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            var subject = new UnitOfWork(dbConnectionMock.Object);

            // Act
            subject.BeginTransaction(isolationLevel);

            // Assert
            dbConnectionMock.Verify(m => m.BeginTransaction(isolationLevel), Times.Once);
            dbConnectionMock.Verify(m => m.BeginTransaction(), Times.Never);
        }
        [Fact]
        public void BeginTransaction_ConnectionBeginTransactionThrowsException_ExceptionResult()
        {
            // Arrange
            var exceptionThrown = new Exception();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Throws(exceptionThrown);
            var subject = new UnitOfWork(dbConnectionMock.Object);

            // Act
            var result = subject.BeginTransaction();

            // Assert
            var exceptionResult = result.Should().BeOfType<ExceptionResult>().Subject;
            exceptionResult.ErrorCode.Should().Contain("Error beginning the transaction");
            exceptionResult.InnerException.Should().Be(exceptionThrown);
        }
        [Fact]
        public void BeginTransaction_CorrectFlow_SuccessResult()
        {
            // Arrange
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction());
            var subject = new UnitOfWork(dbConnectionMock.Object);

            // Act
            var result = subject.BeginTransaction();

            // Assert
            result.Should().BeOfType<SuccessResult>();
        }

        [Fact]
        public void Commit_NoTransactionInProgress_ExceptionResult()
        {
            // Arrange
            var dbConnection = Mock.Of<IDbConnection>();
            var subject = new UnitOfWork(dbConnection);

            // Act
            var result = subject.Commit();

            // Assert
            result.Should().BeOfType<ExceptionResult>()
                  .Which.InnerException.Should().BeOfType<InvalidOperationException>()
                  .Which.Message.Should().Contain("There's no transaction in progress");
        }
        [Fact]
        public void Commit_TransactionInProgress_CallCommitOnTheTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Commit();

            // Assert
            dbTransactionMock.Verify(m => m.Commit(), Times.Once);
        }
        [Fact]
        public void Commit_CommitInTransactionThrowsException_CallRollbackOnTheTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Commit()).Throws<Exception>();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Commit();

            // Assert
            dbTransactionMock.Verify(m => m.Rollback(), Times.Once);
        }
        [Fact]
        public void Commit_RollbackInExceptionThrowsException_ExceptionResultForRollback()
        {
            // Arrange
            var exceptionThrown = new Exception();
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Commit()).Throws<Exception>();
            dbTransactionMock.Setup(m => m.Rollback()).Throws(exceptionThrown);
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Commit();

            // Assert
            var exceptionResult = result.Should().BeOfType<ExceptionResult>().Subject;
            exceptionResult.ErrorCode.Should().Contain("Error trying to rollback a failed committed");
            exceptionResult.InnerException.Should().Be(exceptionThrown);
        }
        [Fact]
        public void Commit_RollbackInExceptionThrowsException_DisposeTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Commit()).Throws<Exception>();
            dbTransactionMock.Setup(m => m.Rollback()).Throws<Exception>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Commit();

            // Assert
            dbTransactionMock.Verify(m => m.Dispose(), Times.Once);
        }
        [Fact]
        public void Commit_RollbackInExceptionThrowsException_CloseConnection()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Commit()).Throws<Exception>();
            dbTransactionMock.Setup(m => m.Rollback()).Throws<Exception>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Commit();

            // Assert
            dbConnectionMock.Verify(m => m.Close(), Times.Once);
        }
        [Fact]
        public void Commit_RollbackInExceptionSuccess_ExceptionResultForCommit()
        {
            // Arrange
            var exceptionThrown = new Exception();
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Commit()).Throws(exceptionThrown);
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Commit();

            // Assert
            var exceptionResult = result.Should().BeOfType<ExceptionResult>().Subject;
            exceptionResult.ErrorCode.Should().Contain("Error committing the transaction");
            exceptionResult.InnerException.Should().Be(exceptionThrown);
        }
        [Fact]
        public void Commit_RollbackInExceptionSuccess_DisposeTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Commit()).Throws<Exception>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Commit();

            // Assert
            dbTransactionMock.Verify(m => m.Dispose(), Times.Once);
        }
        [Fact]
        public void Commit_RollbackInExceptionSuccess_CloseConnection()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Commit()).Throws<Exception>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Commit();

            // Assert
            dbConnectionMock.Verify(m => m.Close(), Times.Once);
        }
        [Fact]
        public void Commit_CommitInTransactionSuccess_SuccessResult()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Commit();

            // Assert
            result.Should().BeOfType<SuccessResult>();
        }
        [Fact]
        public void Commit_CommitInTransactionSuccess_DisposeTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Commit();

            // Assert
            dbTransactionMock.Verify(m => m.Dispose(), Times.Once);
        }
        [Fact]
        public void Commit_CommitInTransactionSuccess_CloseConnection()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Commit();

            // Assert
            dbConnectionMock.Verify(m => m.Close(), Times.Once);
        }

        [Fact]
        public void Rollback_NoTransactionInProgress_ExceptionResult()
        {
            // Arrange
            var dbConnection = Mock.Of<IDbConnection>();
            var subject = new UnitOfWork(dbConnection);

            // Act
            var result = subject.Rollback();

            // Assert
            result.Should().BeOfType<ExceptionResult>()
                  .Which.InnerException.Should().BeOfType<InvalidOperationException>()
                  .Which.Message.Should().Contain("There's no transaction in progress");
        }
        [Fact]
        public void Rollback_TransactionInProgress_CallRollbackOnTheTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);
            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Rollback();

            // Assert
            dbTransactionMock.Verify(m => m.Rollback(), Times.Once);
        }
        [Fact]
        public void Rollback_RollbackInTransactionThrowsException_ExceptionResult()
        {
            // Arrange
            var exceptionThrown = new Exception();
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Rollback()).Throws(exceptionThrown);

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Rollback();

            // Assert
            var exceptionResult = result.Should().BeOfType<ExceptionResult>().Subject;
            exceptionResult.ErrorCode.Should().Contain("Error trying to rollback the transaction");
            exceptionResult.InnerException.Should().Be(exceptionThrown);
        }
        [Fact]
        public void Rollback_RollbackInTransactionThrowsException_DisposeTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Rollback()).Throws<Exception>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Rollback();

            // Assert
            dbTransactionMock.Verify(m => m.Dispose(), Times.Once);
        }
        [Fact]
        public void Rollback_RollbackInTransactionThrowsException_CloseConnection()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();
            dbTransactionMock.Setup(m => m.Rollback()).Throws<Exception>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Rollback();

            // Assert
            dbConnectionMock.Verify(m => m.Close(), Times.Once);
        }
        [Fact]
        public void Rollback_RollbackInTransactionSuccess_SuccessResult()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            var result = subject.Rollback();

            // Assert
            result.Should().BeOfType<SuccessResult>();
        }
        [Fact]
        public void Rollback_RollbackInTransactionSuccess_DisposeTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Rollback();

            // Assert
            dbTransactionMock.Verify(m => m.Dispose(), Times.Once);
        }
        [Fact]
        public void Rollback_RollbackInTransactionSuccess_CloseConnection()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Rollback();

            // Assert
            dbConnectionMock.Verify(m => m.Close(), Times.Once);
        }

        [Fact]
        public void Dispose_InvokeWithTransactionInProgress_DisposeTransaction()
        {
            // Arrange
            var dbTransactionMock = new Mock<IDbTransaction>();

            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock.SetupGet(m => m.State).Returns(ConnectionState.Open);
            dbConnectionMock.Setup(m => m.BeginTransaction()).Returns(dbTransactionMock.Object);

            var subject = new UnitOfWork(dbConnectionMock.Object);
            subject.BeginTransaction();

            // Act
            subject.Dispose();

            // Assert
            dbTransactionMock.Verify(m => m.Dispose(), Times.Once);
        }
        [Fact]
        public void Dispose_Invoke_DisposeConnection()
        {
            // Arrange
            var dbConnectionMock = new Mock<IDbConnection>();
            var subject = new UnitOfWork(dbConnectionMock.Object);

            // Act
            subject.Dispose();

            // Assert
            dbConnectionMock.Verify(m => m.Dispose(), Times.Once);
        }
    }
}
