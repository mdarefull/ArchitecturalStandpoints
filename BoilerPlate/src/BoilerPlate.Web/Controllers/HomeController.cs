using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.Web.Controllers
{
    public class HomeController : BoilerPlateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}