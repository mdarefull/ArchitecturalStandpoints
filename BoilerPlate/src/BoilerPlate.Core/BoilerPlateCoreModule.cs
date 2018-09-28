using Abp.Modules;
using Abp.Reflection.Extensions;
using BoilerPlate.Localization;

namespace BoilerPlate
{
    public class BoilerPlateCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            BoilerPlateLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerPlateCoreModule).GetAssembly());
        }
    }
}