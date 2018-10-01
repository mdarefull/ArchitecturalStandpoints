using ArchitecturalStandpoints.Sales;
using Commons.OperationResult;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Tests
{
    /// <summary>
    /// Dummy service for testing purposes.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class DummyServiceController : ControllerBase
    {
        private ISalesService SalesService { get; }
        public DummyServiceController(ISalesService salesService) => SalesService = salesService;
        /// <summary>
        /// Simple operation that greets someone.
        /// </summary>
        /// <param name="toWho">Name of the person to greet.</param>
        /// <returns>Message greeting the given person's name or anonymously greeting if no name is provided.</returns>
        [HttpGet]
        public async Task<ActionResult<OperationResult<string>>> GreetAsync(string toWho)
        {
            var result = OperationResult.Success($"Greetings {toWho ?? "(anonymous guy)"}!");
            return await Task.FromResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResult<GetSalesByCategoryOutput>>> GetSalesByCategoryAsync(string categoryName, int year)
        {
            var input = new GetSalesByCategoryInput
            {
                CategoryName = categoryName,
                OrdYear = year
            };
            var result = await SalesService.GetSalesByCategoryAsync(input);
            return result.ToResult(new GetSalesByCategoryOutput { Sales = result.Result });
        }
    }
}