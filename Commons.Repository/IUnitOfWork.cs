using Commons.OperationResult;
using System;
using System.Data;

namespace Commons.Repository
{
    /// <summary>
    /// Defines the contract for the Unit of Work pattern.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begins a new transaction.
        /// </summary>
        /// <param name="isolationLevel">
        /// Specifies the transaction locking behavior for the transaction.
        /// It depends on the underlying database providers.
        /// </param>
        /// <returns><see cref="SuccessResult"/> if the transaction could be started, <see cref="FailureResult"/> otherwise.</returns>
        /// <remarks>
        /// If this method is invoked when a transaction is already in progress, 
        /// an <see cref="ExceptionResult"/> will be generated wrapping an <see cref="InvalidOperationException"/>.
        /// </remarks>
        IResult BeginTransaction(IsolationLevel? isolationLevel = null);
        /// <summary>
        /// Commit the currently in progress transaction.
        /// </summary>
        /// <returns><see cref="SuccessResult"/> if the current in progress transaction was committed successfully, <see cref="FailureResult"/> otherwise.</returns>
        /// <remarks>
        /// If this method is invoked when there's no transaction in progress, an <see cref="ExceptionResult"/> will be generated
        /// wrapping an <see cref="InvalidOperationException"/>.
        /// </remarks>
        IResult Commit();
        /// <summary>
        /// Rollbacks the currently in progress transaction.
        /// </summary>
        /// <returns><see cref="SuccessResult"/> if the current in progress transaction was committed successfully, <see cref="FailureResult"/> otherwise.</returns>
        /// <remarks>
        /// If this method is invoked when there's no transaction in progress, an <see cref="ExceptionResult"/> will be generated
        /// wrapping an <see cref="InvalidOperationException"/>.
        /// </remarks>
        IResult Rollback();
    }
}
