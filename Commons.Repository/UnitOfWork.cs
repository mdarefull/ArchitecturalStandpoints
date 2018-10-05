using Commons.OperationResult;
using System.Data;

namespace Commons.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public UnitOfWork(IDbConnection connection) => Connection = connection;

        // [TODO] The default IL value is DB-specific.
        public IResult<IDbTransaction> BeginTransaction(IsolationLevel? isolationLevel = null)
        {
            // [TODO] Improve this, we should check and understand each one of the Connection.State values.
            Connection.Open();

            var transaction = isolationLevel.HasValue
                            ? Connection.BeginTransaction(isolationLevel.Value)
                            : Connection.BeginTransaction();
            return Result.Success(transaction);
        }

        public IResult Commit()
        {
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                DisposeTransaction();
                Connection.Close();
            }

            return Result.Success();
        }

        public IResult Rollback()
        {
            try
            {
                Transaction.Rollback();
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeTransaction();
                    DisposeConnection();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        protected virtual void DisposeTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }
        protected virtual void DisposeConnection()
        {
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() =>
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);// TODO: uncomment the following line if the finalizer is overridden above.// GC.SuppressFinalize(this);
        #endregion
    }
}
