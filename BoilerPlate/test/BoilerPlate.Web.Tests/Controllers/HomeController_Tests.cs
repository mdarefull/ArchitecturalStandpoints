using System.Threading.Tasks;
using BoilerPlate.Web.Controllers;
using Shouldly;
using Xunit;

namespace BoilerPlate.Web.Tests.Controllers
{
    public class HomeController_Tests: BoilerPlateWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
