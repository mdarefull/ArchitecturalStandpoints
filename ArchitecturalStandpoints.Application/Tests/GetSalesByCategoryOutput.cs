using ArchitecturalStandpoints.Sales;
using System.Collections.Generic;

namespace ArchitecturalStandpoints.Tests
{
    public class GetSalesByCategoryOutput
    {
        public IReadOnlyCollection<SaleByCategory> Sales { get; set; }
    }
}
