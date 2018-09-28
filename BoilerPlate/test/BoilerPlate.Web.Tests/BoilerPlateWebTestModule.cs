using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BoilerPlate.Web.Startup;
namespace BoilerPlate.Web.Tests
{
    [DependsOn(
        typeof(BoilerPlateWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class BoilerPlateWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerPlateWebTestModule).GetAssembly());
        }
    }
}