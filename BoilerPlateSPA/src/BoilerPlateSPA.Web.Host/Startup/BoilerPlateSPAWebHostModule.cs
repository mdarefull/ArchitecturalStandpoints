using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BoilerPlateSPA.Configuration;

namespace BoilerPlateSPA.Web.Host.Startup
{
    [DependsOn(
       typeof(BoilerPlateSPAWebCoreModule))]
    public class BoilerPlateSPAWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BoilerPlateSPAWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerPlateSPAWebHostModule).GetAssembly());
        }
    }
}
