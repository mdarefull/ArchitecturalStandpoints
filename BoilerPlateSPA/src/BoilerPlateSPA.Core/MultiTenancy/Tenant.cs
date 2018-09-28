using Abp.MultiTenancy;
using BoilerPlateSPA.Authorization.Users;

namespace BoilerPlateSPA.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
