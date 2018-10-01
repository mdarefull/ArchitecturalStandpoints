using Commons.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Sales
{
    public interface ISalesService
    {
        Task<OperationResult<IReadOnlyCollection<SaleByCategory>>> GetSalesByCategoryAsync(GetSalesByCategoryInput input);
    }
}
