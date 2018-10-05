using ArchitecturalStandpoints.Customers;
using Commons.OperationResult;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Application.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CustomerController : ControllerBase
    {
        private ICustomerRepository Repository { get; }
        public CustomerController(ICustomerRepository repository) => Repository = repository;

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
                return NotFound(id);
            }

            if (result is ExceptionResult exception)
            {
                return StatusCode(500, "Unexpected Exception");
            }

            return StatusCode(500, "Unexpected Return Type");
        }

        // POST: api/Customer
        [HttpPost]
        public void Post(Customer customer) => new NotImplementedException();

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) => new NotImplementedException();

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id) => new NotImplementedException();
    }
}
