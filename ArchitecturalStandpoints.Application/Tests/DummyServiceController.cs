using ArchitecturalStandpoints.Sales;
using Commons.OperationResult;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
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
        private IDbConnection DbConnection { get; }
        private IDummyDisposable DummyDisposable { get; }
        public DummyServiceController(ISalesService salesService, IDbConnection dbConnection, IDummyDisposable dummyDisposable)
        {
            SalesService = salesService;
            DbConnection = dbConnection;
            DummyDisposable = dummyDisposable;
        }

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

        [HttpGet]
        public async Task<IEnumerable<Test>> TestTransactions(int option)
        {
            // Ensures there's no data on the table:
            await DbConnection.ExecuteAsync("DELETE FROM Test");

            // Begin the transaction:
            // !!! Need to be opened before begining the transaction.
            DbConnection.Open();
            var transaction = DbConnection.BeginTransaction();

            // Inserts 10 tests:
            for (var i = 0; i < 10; i++)
            {
                var test = new Test
                {
                    Name = $"Test - {i}",
                };
                await DbConnection.ExecuteAsync(sql: @"INSERT INTO Test ([Name]) VALUES (@Name)",
                                                param: test,
                                                transaction: transaction);
            }

            // Terminate the transaction
            if (option == 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            // Return the table content:
            return DbConnection.Query<Test>("SELECT * FROM TEST");
        }
    }
}