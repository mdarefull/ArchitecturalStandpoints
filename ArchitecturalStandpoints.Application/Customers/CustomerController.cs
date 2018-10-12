using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commons.OperationResult;
using Commons.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArchitecturalStandpoints.Customers
{
    /// <summary>
    /// Represents the set of application's services for a Customer.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CustomerController : ControllerBase
    {
        private ILogger Logger { get; }
        private IUnitOfWork UnitOfWork { get; }
        private ICustomerRepository Repository { get; }
        /// <summary>
        /// Creates a new instance of a <see cref="CustomerController"/>
        /// </summary>
        /// <param name="repository">Customer repository implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        /// <param name="unitOfWork">Unit of Work implementation.</param>
        public CustomerController(ICustomerRepository repository,
                                  ILogger<CustomerController> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        // GET: api/Customer
        /// <summary>
        /// Get the list of customers.
        /// </summary>
        /// <returns>List of customers of the application.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var result = await Repository.GetAllAsync();
            switch (result)
            {
                case SuccessResult<IEnumerable<Customer>> success:
                    return Ok(success.Value);
                case ExceptionResult _:
                    return StatusCode(500, "Unexpected Exception");
                default:
                    return StatusCode(500, "Unexpected Return Type");
            }
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

            Logger.LogError($"Unexpected Return Type: {result}");
            return StatusCode(501);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public Customer Put(int id, Customer customer) => throw new NotImplementedException();

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id) => throw new NotImplementedException();
    }
}
