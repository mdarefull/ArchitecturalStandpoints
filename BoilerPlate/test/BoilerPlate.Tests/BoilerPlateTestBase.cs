using System;
using System.Threading.Tasks;
using Abp.TestBase;
using BoilerPlate.EntityFrameworkCore;
using BoilerPlate.Tests.TestDatas;

namespace BoilerPlate.Tests
{
    public class BoilerPlateTestBase : AbpIntegratedTestBase<BoilerPlateTestModule>
    {
        public BoilerPlateTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<BoilerPlateDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<BoilerPlateDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<BoilerPlateDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<BoilerPlateDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<BoilerPlateDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<BoilerPlateDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<BoilerPlateDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<BoilerPlateDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
