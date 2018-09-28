using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace BoilerPlate
{
    [DependsOn(
        typeof(BoilerPlateCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BoilerPlateApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerPlateApplicationModule).GetAssembly());
        }
    }
}