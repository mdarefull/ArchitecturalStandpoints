using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ABP.Authorization.Roles;
using ABP.Authorization.Users;
using ABP.MultiTenancy;

namespace ABP.EntityFrameworkCore
{
    public class ABPDbContext : AbpZeroDbContext<Tenant, Role, User, ABPDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ABPDbContext(DbContextOptions<ABPDbContext> options)
            : base(options)
        {
        }
    }
}
