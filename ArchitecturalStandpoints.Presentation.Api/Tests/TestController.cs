﻿using ArchitecturalStandpoints.Tests;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Api.Tests
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class TestController : ControllerBase
    {
        public IDummyService DummyService { get; }
        public TestController(IDummyService dummyService) => DummyService = dummyService;

        [HttpGet]
        public async Task<ActionResult<string>> SayHello(string name)
        {
            var greetResult = await DummyService.GreetAsync(toWho: name);
            return greetResult.Result;
        }

        [HttpGet]
        public async Task<ActionResult<string[]>> GetSalesByCategory(string name)
        {
            var salesResult = await DummyService.GetSalesByCategoryAsync(name, 1998);
            return salesResult
                   .Result
                   .Sales
                   .Select(s => s.ProductName)
                   .ToArray();
        }
    }
}