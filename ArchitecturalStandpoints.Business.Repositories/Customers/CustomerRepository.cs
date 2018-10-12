using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Commons.OperationResult;
using Commons.Repository;

using Dapper;

namespace ArchitecturalStandpoints.Customers
{
    public class CustomerRepository : RepositoryBase<Customer, string>, ICustomerRepository
    {
        public CustomerRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<IResult<Customer>> GetByIdAsync(string id)
        {
            var sql = "SELECT TOP(1) * FROM Customers WHERE CustomerId = @Id";
            try
            {
                var customer = await Connection
                                    .QueryFirstOrDefaultAsync<Customer>(
                                        sql: sql,
                                        param: new { id },
                                        transaction: Transaction);
                return customer == null
                    ? Result.NotFound<Customer>()
                    : Result.Success(customer);
            }
            catch (Exception e)
            {
                return Result.Exception<Customer>(e);
            }
        }

        public override async Task<IResult<IEnumerable<Customer>>> GetAllAsync()
        {
            var sql = "SELECT * FROM Customers";
            try
            {
                var customers = await Connection
                                     .QueryAsync<Customer>(
                                        sql: sql,
                                        transaction: Transaction);
                return Result.Success(customers);
            }
            catch (Exception e)
            {
                return Result.Exception<IEnumerable<Customer>>(e);
            }
        }

        public override async Task<IResult<Customer>> AddAsync(Customer entity)
        {
            var sql = @"INSERT INTO [dbo].[Customers]
                                   ([CustomerID]
                                   ,[CompanyName]
                                   ,[ContactName]
                                   ,[ContactTitle]
                                   ,[Address]
                                   ,[City]
                                   ,[Region]
                                   ,[PostalCode]
                                   ,[Country]
                                   ,[Phone]
                                   ,[Fax])
                             VALUES
                                   (@CustomerID
                                   ,@CompanyName
                                   ,@ContactName
                                   ,@ContactTitle
                                   ,@Address
                                   ,@City
                                   ,@Region
                                   ,@PostalCode
                                   ,@Country
                                   ,@Phone
                                   ,@Fax)";
            try
            {
                var rows = await Connection
                                .ExecuteAsync(
                                    sql: sql,
                                    param: entity,
                                    transaction: Transaction);
                return rows == 1
                    ? Result.Success(entity)
                    : Result.Failure<Customer>("Could not insert");
            }
            catch (Exception e)
            {
                return Result.Exception<Customer>(e);
            }
        }

        public override Task<IResult<Customer>> UpdateAsync(Customer entity) => throw new NotImplementedException();
        public override Task<IResult<Customer>> RemoveAsync(Customer entity) => throw new NotImplementedException();
        public override Task<IResult<Customer>> RemoveByIdAsync(string id) => throw new NotImplementedException();
    }
}
