using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlateSPA.Controllers
{
    public abstract class BoilerPlateSPAControllerBase: AbpController
    {
        protected BoilerPlateSPAControllerBase()
        {
            LocalizationSourceName = BoilerPlateSPAConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
