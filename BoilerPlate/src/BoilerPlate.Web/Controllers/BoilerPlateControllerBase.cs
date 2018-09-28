using Abp.AspNetCore.Mvc.Controllers;

namespace BoilerPlate.Web.Controllers
{
    public abstract class BoilerPlateControllerBase: AbpController
    {
        protected BoilerPlateControllerBase()
        {
            LocalizationSourceName = BoilerPlateConsts.LocalizationSourceName;
        }
    }
}