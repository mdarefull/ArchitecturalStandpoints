using System;
using System.Data;

using Commons.OperationResult;

namespace Commons.Repository
{
    /// <summary>
    /// Implementation of <see cref="IUnitOfWork"/> to work with ADO.NET's <see cref="IDbConnection"/>.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the <see cref="IDbConnection"/>'s instance that 
        /// represents the connection to the underlying Database.
        /// </summary>
        protected internal virtual IDbConnection Connection { get; }
        /// <summary>
        /// Gets the <see cref="IDbTransaction"/>'s instance that
        /// represents the current transaction in progress.
        /// <code>null</code> if there's no transaction in progress for the
        /// <see cref="Connection"/>.
        /// </summary>
        protected internal virtual IDbTransaction Transaction { get; protected set; }
        /// <summary>
        /// Creates a new instance of <see cref="UnitOfWork"/>.
        /// </summary>
        /// <param name="connection">
        /// <see cref="IDbConnection"/>'s instance to be used to perform all Db operation that 
        /// this UoW will relate to.
        /// </param>
        public UnitOfWork(IDbConnection connection) => Connection = connection;

        /// <inheritdoc />
        public virtual IResult BeginTransaction(IsolationLevel? isolationLevel = null)
        {
            if (Transaction != null)
            {
                return Result.Exception(new InvalidOperationException("There's a transaction already in progress."));
            }

            if (Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();
                }
                catch (Exception e)
                {
                    return new ExceptionResult
                    {
                        ErrorCode = "Error opening the connection.",
                        InnerException = e,
                    };
                }
            }

            try
            {
                Transaction = isolationLevel.HasValue
                            ? Connection.BeginTransaction(isolationLevel.Value)
                            : Connection.BeginTransaction();
            }
            catch (Exception e)
            {
                return new ExceptionResult
                {
                    ErrorCode = "Error beginning the transaction.",
                    InnerException = e,
                };
            }

            return Result.Success();
        }

        /// <inheritdoc />
        public virtual IResult Commit()
        {
            if (Transaction == null)
            {
                return Result.Exception(new InvalidOperationException("There's no transaction in progress."));
            }

            try
            {
                Transaction.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    Transaction.Rollback();
                    return new ExceptionResult
                    {
                        ErrorCode = "Error committing the transaction.",
                        InnerException = e,
                    };
                }
                catch (Exception e2)
                {
                    return new ExceptionResult
                    {
                        ErrorCode = "Error trying to rollback a failed committed.",
                        InnerException = e2,
                    };
                }
            }
            finally
            {
                DisposeTransaction();
                Connection.Close();
            }

            return Result.Success();
        }

        /// <inheritdoc />
        public virtual IResult Rollback()
        {
            if (Transaction == null)
            {
                return Result.Exception(new InvalidOperationException("There's no transaction in progress."));
            }

            try
            {
                Transaction.Rollback();
            }
            catch (Exception e)
            {
                return new ExceptionResult
                {
                    ErrorCode = "Error trying to rollback the transaction.",
                    InnerException = e,
                };
            }
            finally
            {
                DisposeTransaction();
                Connection.Close();
            }

            return Result.Success();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        /// <summary>
        /// Automatically generated implementation of the IDisposable pattern.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeTransaction();
                    Connection.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        /// <summary>
        /// Automatically generated implementation of the IDisposable pattern.
        /// </summary>
        protected virtual void DisposeTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        /// <summary>
        /// Automatically generated implementation of the IDisposable pattern.
        /// </summary>
        // This code added to correctly implement the disposable pattern.
        public void Dispose() =>
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);// TODO: uncomment the following line if the finalizer is overridden above.// GC.SuppressFinalize(this);
        #endregion
    }
}
