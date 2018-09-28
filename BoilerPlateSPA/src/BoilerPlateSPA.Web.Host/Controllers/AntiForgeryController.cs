using Microsoft.AspNetCore.Antiforgery;
using BoilerPlateSPA.Controllers;

namespace BoilerPlateSPA.Web.Host.Controllers
{
    public class AntiForgeryController : BoilerPlateSPAControllerBase
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
