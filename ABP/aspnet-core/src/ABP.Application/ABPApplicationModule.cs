using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABP.Authorization;

namespace ABP
{
    [DependsOn(
        typeof(ABPCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ABPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ABPAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ABPApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
