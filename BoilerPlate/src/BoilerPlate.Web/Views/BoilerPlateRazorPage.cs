using Abp.AspNetCore.Mvc.Views;

namespace BoilerPlate.Web.Views
{
    public abstract class BoilerPlateRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected BoilerPlateRazorPage()
        {
            LocalizationSourceName = BoilerPlateConsts.LocalizationSourceName;
        }
    }
}
