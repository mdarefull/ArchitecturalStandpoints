using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace BoilerPlate.EntityFrameworkCore
{
    [DependsOn(
        typeof(BoilerPlateCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class BoilerPlateEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerPlateEntityFrameworkCoreModule).GetAssembly());
        }
    }
}