using Commons.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Sales
{
    public interface ISalesService
    {
        Task<Result<IReadOnlyCollection<SaleByCategory>>> GetSalesByCategoryAsync(GetSalesByCategoryInput input);
    }
}
