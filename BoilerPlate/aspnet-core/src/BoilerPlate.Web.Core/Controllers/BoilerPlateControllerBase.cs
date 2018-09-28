using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Controllers
{
    public abstract class BoilerPlateControllerBase: AbpController
    {
        protected BoilerPlateControllerBase()
        {
            LocalizationSourceName = BoilerPlateConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
