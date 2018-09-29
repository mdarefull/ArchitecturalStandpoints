using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<GreetResult>> GreetAsync(string toWho)
        {
            var result = new GreetResult
            {
                ResultType = GreetResults.Success,
                Message = $"Greetings {toWho ?? "(anonymous guy)"}!",
            };
            return await Task.FromResult(result);
        }
    }

    /// <summary>
    /// Represents the result for the operation: <see cref="DummyServiceController.GreetAsync(string)"/>
    /// </summary>
    public sealed class GreetResult
    {
        /// <summary>
        /// Indicates the type of the result.
        /// </summary>
        public GreetResults ResultType { get; set; }
        /// <summary>
        /// If <see cref="GreetResults"/> is <see cref="GreetResults.Success"/> then contains the 
        /// returned message from the operation. <code>null</code> otherwise.
        /// </summary>
        public string Message { get; set; }
    }
    /// <summary>
    /// Represents the set of possible results for the operation <see cref="DummyServiceController.GreetAsync(string)"/>
    /// </summary>
    public enum GreetResults : byte
    {
        /// <summary>
        /// Indicates that the operation has been sucesfully executed.
        /// </summary>
        Success = 0,
        /// <summary>
        /// Indicates that the operation has failed unexpectedly.
        /// </summary>
        ErrorGeneric,
    }
}