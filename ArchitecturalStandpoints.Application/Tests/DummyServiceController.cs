using ArchitecturalStandpoints.Application.Models;
using ArchitecturalStandpoints.Sales;
using Commons.OperationResult;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
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
        #region Boiler Plate
        private ISalesService SalesService { get; }
        private IDbConnection DbConnection { get; }
        private IDummyDisposable DummyDisposable { get; }
        private NorthwindContext Context { get; set; }
        public DummyServiceController(ISalesService salesService, IDbConnection dbConnection, IDummyDisposable dummyDisposable, NorthwindContext context)
        {
            SalesService = salesService;
            DbConnection = dbConnection;
            DummyDisposable = dummyDisposable;
            Context = context;
        }

        /// <summary>
        /// Simple operation that greets someone.
        /// </summary>
        /// <param name="toWho">Name of the person to greet.</param>
        /// <returns>Message greeting the given person's name or anonymously greeting if no name is provided.</returns>
        [HttpGet]
        public async Task<ActionResult<OperationResult<string>>> Greet(string toWho)
        {
            var result = OperationResult.Success($"Greetings {toWho ?? "(anonymous guy)"}!");
            return await Task.FromResult(result);
        } 
        #endregion

        #region Dapper Samples
        [HttpGet]
        public async Task<IEnumerable<SaleByCategory>> GetSalesByCategory(string categoryName, int year) =>
            await DbConnection
                 .QueryAsync<SaleByCategory>(
                    sql: "SalesByCategory",
                    param: new { categoryName, OrdYear = year },
                    commandType: CommandType.StoredProcedure);

        // WARNING - The affected tables are blocked through the whole lifespan of the transaction.
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
                var name = $"Test - {i}";
                await DbConnection.ExecuteAsync(sql: "CreateTest",
                                                param: new { name },
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

        //[HttpGet]
        //public async Task<IEnumerable<Test>> MultiMapping() =>
        //    // Not implemented - This is a really complex case.
        //    // Case: I have a task, this task has a parent task. Given a task, I need to update all its tree
        //    // 1- If the task has a parent task, I need to check it.
        //    // 2- If the task doesn't have a parent task, then I check if it is new (Id = 0) or exists (Id != 0)
        //    // 3- For each case, I update the task accordingly (create or update)
        //    // 4- Then I move to its child task (step 2) assuming it has no parent because it was already checked.

        //    // For retrieval, the most I could expect was to use Multi Mapping Dapper feature: 
        //    // https://github.com/StackExchange/Dapper#multi-mapping
        //    // This allows me to map a record to multiple classes, on this case, I will receive a Task, with another task inside
        //    // (its parent)... but... sadly... this "parent task", will not have a parent task set.
        //    // So basically the SPs needs to return one record per task, with the information associated to the parent task.
        //    // Then, in code, we need to travel the collection to "remove" this duplicates:
        //    // 1- Find the tasks whose parent is null.
        //    // 2- For each of this task, find the tasks that have it as a parent.
        //    // 3- Set the task as the actual parent of the tasks.
        //    // 4- Perform the same operation to all the children of these tasks (moving to step 2)
        //    // Complexity O(n2) in both places, Server and DB...
        //    throw new NotImplementedException();
        #endregion

        #region Entity Framework samples
        // Simply to check that the Db scaffolding worked.
        [HttpGet]
        public async Task<IEnumerable<Products>> EF_GetProducts() => await Context.Products.ToListAsync();

        // Show how EF works with SPs - Note it is much more verbose.
        [HttpGet]
        public async Task<IEnumerable<SaleByCategory>> EF_SalesByCategory()
        {
            var categoryParam = new SqlParameter("CategoryName", "Beverages");
            var yearParam = new SqlParameter("OrdYear", 1998);
            return await Context
                        .SalesByCategory
                        .FromSql(
                            sql: $"EXECUTE dbo.SalesByCategory @CategoryName, @OrdYear",
                            categoryParam, yearParam)
                        .ToListAsync();
        }

        // Built-in Transactions do not work using SPs.
        [HttpGet]
        public async Task<IEnumerable<Test>> EF_Transactions(int option)
        {
            // Ensure there's no data on the table:
            // Here I'm using Dapper as a shortcut, to avoid using EF and to interfere with its transactions.
            await DbConnection.ExecuteAsync("DELETE FROM Test");

            // Inserts 10 tests:
            for (var i = 0; i < 10; i++)
            {
                var nameParam = new SqlParameter("name", $"Test - {i}");
                await Context.Database.ExecuteSqlCommandAsync("EXECUTE dbo.CreateTest @name", nameParam);
            }

            // Let's insert a test using the DbContext:
            Context.Test.Add(new Test { Name = "Test XXXX" });

            if (option == 0)
            {
                await Context.SaveChangesAsync();
            }

            // Ensures the context is disposed.
            Context.Dispose();

            // Return the table content, ensuring the entities won't be cached by EF
            return DbConnection.Query<Test>("SELECT * FROM TEST");
        }

        // We can modify query types after retrieving them. Their state won't be tracked.
        [HttpGet]
        public async Task<SaleByCategory> EF_ModifyAQueryType()
        {
            var sales = await EF_SalesByCategory();
            var firstSale = sales.FirstOrDefault();
            if (firstSale != null)
            {
                firstSale.ProductName = "Another name hehehe";
                await Context.SaveChangesAsync();
            }

            return firstSale;
        }

        // Query Caching Tests Vs Sales report GetAll
        // Apparently both are executed twice.
        [HttpGet]
        public async Task<string> Ef_CachingAll()
        {
            var products = await EF_GetProducts();
            //Thread.Sleep(1000);
            var tests2 = await EF_GetProducts();

            var sales = await EF_SalesByCategory();
            //Thread.Sleep(1000);
            var sales2 = await EF_SalesByCategory();

            return "Done";
        }
        // Same, executed twice...
        [HttpGet]
        public async Task<string> Ef_CachingOne()
        {
            var productA = await Context.Products.FirstOrDefaultAsync();
            var productB = await Context.Products.FirstOrDefaultAsync();

            return "Done";
        }
        #endregion
    }
}