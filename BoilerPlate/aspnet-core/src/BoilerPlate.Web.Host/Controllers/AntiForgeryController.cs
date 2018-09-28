using Microsoft.AspNetCore.Antiforgery;
using BoilerPlate.Controllers;

namespace BoilerPlate.Web.Host.Controllers
{
    public class AntiForgeryController : BoilerPlateControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
