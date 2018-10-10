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
        /// <inheritdoc />
        public virtual IDbConnection Connection { get; }
        /// <inheritdoc />
        public virtual IDbTransaction Transaction { get; protected set; }
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

            // [TODO] Improve this, we should check and understand each one of the Connection.State values.
            Connection.Open();

            var transaction = isolationLevel.HasValue
                            ? Connection.BeginTransaction(isolationLevel.Value)
                            : Connection.BeginTransaction();
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
                Transaction.Rollback();
                return Result.Exception(e);
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
                return Result.Exception(e);
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
