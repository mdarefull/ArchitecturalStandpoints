using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BoilerPlate.Configuration;
using BoilerPlate.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BoilerPlate.Web.Startup
{
    [DependsOn(
        typeof(BoilerPlateApplicationModule), 
        typeof(BoilerPlateEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class BoilerPlateWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public BoilerPlateWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(BoilerPlateConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<BoilerPlateNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(BoilerPlateApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerPlateWebModule).GetAssembly());
        }
    }
}