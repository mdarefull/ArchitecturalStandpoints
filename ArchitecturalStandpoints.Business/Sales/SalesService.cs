using Commons.OperationResult;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Sales
{
    public class SalesService : ISalesService
    {
        protected virtual IDbConnection DbConnection { get; }
        public SalesService(IDbConnection dbConnection) => DbConnection = dbConnection;

        public virtual async Task<Result<IReadOnlyCollection<SaleByCategory>>> GetSalesByCategoryAsync(GetSalesByCategoryInput input)
        {
            var spResult = await DbConnection
                                 .QueryAsync<SaleByCategory>(
                                   "SalesByCategory",
                                   input,
                                   commandType: CommandType.StoredProcedure);
            return Result.Success<IReadOnlyCollection<SaleByCategory>>(spResult.ToArray());
        }
    }
}
