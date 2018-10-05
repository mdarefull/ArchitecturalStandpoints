using ArchitecturalStandpoints.Customers;
using Commons.Api;
using Commons.OperationResult;
using Commons.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Application.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CustomerController : BaseController
    {
        private ICustomerRepository Repository { get; }
        public CustomerController(ICustomerRepository repository,
                                  ILogger<CustomerController> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork) => Repository = repository;

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var result = await Repository.GetAllAsync();
            if (result is SuccessResult<IEnumerable<Customer>> success)
            {
                return Ok(success.Value);
            }

            if (result is ExceptionResult exception)
            {
                return StatusCode(500, "Unexpected Exception");
            }

            return StatusCode(500, "Unexpected Return Type");
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var result = await Repository.GetByIdAsync(id);
            if (result is SuccessResult<Customer> success)
            {
                return success.Value;
            }

            if (result is NotFoundResult<Customer>)
            {
                Logger.LogWarning(nameof(NotFoundResult<Customer>), id);
                return NotFound(id);
            }

            if (result is ExceptionResult exception)
            {
                Logger.LogError(nameof(ExceptionResult), exception.ErrorTitle, exception.ErrorDescription, exception.InnerException);
                return StatusCode(500, "Unexpected Exception");
            }

            Logger.LogError("Unexpected Return Type", result);
            return StatusCode(500, "Unexpected Return Type");
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer customer)
        {
            UnitOfWork.BeginTransaction();

            var result = await Repository.AddAsync(customer);
            if (result is SuccessResult<Customer> success)
            {
                UnitOfWork.Commit();
                return success.Value;
            }

            Logger.LogError("Unexpected Return Type", result);
            return StatusCode(501);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) => new NotImplementedException();

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id) => new NotImplementedException();
    }
}
