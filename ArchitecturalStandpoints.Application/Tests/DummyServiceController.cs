using Commons.OperationResult;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
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
        /// <summary>
        /// Simple operation that greets someone.
        /// </summary>
        /// <param name="toWho">Name of the person to greet.</param>
        /// <returns>Message greeting the given person's name or anonymously greeting if no name is provided.</returns>
        [HttpGet]
        public async Task<ActionResult<BasicOperationResult<string>>> GreetAsync(string toWho)
        {
            var result = BasicOperationResult.Success($"Greetings {toWho ?? "(anonymous guy)"}!");
            return await Task.FromResult(result);
        }


        void method()
        {
            var x = new System.Data.SqlClient.SqlConnection("Server=(LocalDb)\\MSSQLLocalDB; Database=BoilerPlateDb; Trusted_Connection=True;");
            x.
        }
    }
}