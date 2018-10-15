using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ArchitecturalStandpoints.Customers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace ArchitecturalStandpoints.Application.FunctionalTests.Customers
{
    public class CustomerControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private WebApplicationFactory<Startup> Factory { get; }
        public CustomerControllerTest(WebApplicationFactory<Startup> factory) => Factory = factory;

        // [TODO] Once we finish with setting a test db, we can ensure how many data are we going to receive
        //  and we can even compare it.
        [Fact]
        public async Task Get_Invoke_ReturnAllCustomers()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync("api/Customer/");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(content);
            customers.Should().NotBeEmpty()
                .And.AllBeOfType<Customer>();
        }
    }
}
