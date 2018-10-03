using ArchitecturalStandpoints.Sales;
using Commons.OperationResult;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                await DbConnection.ExecuteAsync(sql: "CreateTest",
                                                param: new { test.Name },
                                                transaction: transaction,
                                                commandType: CommandType.StoredProcedure);
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
            //return Enumerable.Empty<Test>();
        }

        [HttpGet]
        public async Task<IEnumerable<Test>> MultiMapping()
        {
            // Not implemented - This is a really complex case.
            // Case: I have a task, this task has a parent task. Given a task, I need to update all its tree
            // 1- If the task has a parent task, I need to check it.
            // 2- If the task doesn't have a parent task, then I check if it is new (Id = 0) or exists (Id != 0)
            // 3- For each case, I update the task accordingly (create or update)
            // 4- Then I move to its child task (step 2) assuming it has no parent because it was already checked.

            // For retrieval, the most I could expect was to use Multi Mapping Dapper feature: 
            // https://github.com/StackExchange/Dapper#multi-mapping
            // This allows me to map a record to multiple classes, on this case, I will receive a Task, with another task inside
            // (its parent)... but... sadly... this "parent task", will not have a parent task set.
            // So basically the SPs needs to return one record per task, with the information associated to the parent task.
            // Then, in code, we need to travel the collection to "remove" this duplicates:
            // 1- Find the tasks whose parent is null.
            // 2- For each of this task, find the tasks that have it as a parent.
            // 3- Set the task as the actual parent of the tasks.
            // 4- Perform the same operation to all the children of these tasks (moving to step 2)
            // Complexity O(n2) in both places, Server and DB...
            throw new NotImplementedException();
        }
    }
}