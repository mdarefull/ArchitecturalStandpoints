using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Tests
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class DummyServiceController : ControllerBase, IDummyService
    {
        [HttpGet]
        public Task<GreetOutput> Greet([FromQuery] GreetInput input)
        {
            var result = new GreetOutput
            {
                ResultType = GreetResults.Success,
                Message = $"Greetings {input.ToWho ?? "(anonymous guy)"}!",
            };
            return Task.FromResult(result);
        }
    }
}