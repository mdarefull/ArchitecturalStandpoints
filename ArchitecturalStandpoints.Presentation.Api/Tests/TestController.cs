using ArchitecturalStandpoints.Core;
using ArchitecturalStandpoints.Tests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Api.Tests
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<string> SayHello(string name)
        {
            var serviceResolver = new InMemoryServiceOperationResolver();
            var service = new DummyServiceProxy(serviceResolver);
            var greetInpu = new GreetInput { ToWho = name, };

            var greetOutput = await service.Greet(greetInpu);
            return greetOutput.Message;
        }
    }
}