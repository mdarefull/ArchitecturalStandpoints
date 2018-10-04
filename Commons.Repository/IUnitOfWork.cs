using Commons.OperationResult;
using System;
using System.Data;

namespace Commons.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get; set; }

        Result<IDbTransaction> BeginTransaction(IsolationLevel? isolationLevel = null);
        Result Commit();
        Result Rollback();
    }
}
