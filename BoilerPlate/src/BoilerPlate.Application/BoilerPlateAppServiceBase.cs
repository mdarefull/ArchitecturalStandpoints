using Abp.Application.Services;

namespace BoilerPlate
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BoilerPlateAppServiceBase : ApplicationService
    {
        protected BoilerPlateAppServiceBase()
        {
            LocalizationSourceName = BoilerPlateConsts.LocalizationSourceName;
        }
    }
}